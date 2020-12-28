using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YY.TechJournalReaderAssistant.EventArguments;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant
{
    public class TechJournalReader : ITechJournalReader, IDisposable
    {
        #region Public Static Methods

        public static TechJournalReader CreateReader(string pathLogFile)
        {
            return new TechJournalReader(pathLogFile);
        }
        
        #endregion

        #region Private Member Variables

        private readonly string _logFileDirectoryPath;
        private string[] _logFilesWithData;
        private int _indexCurrentFile;
        private long _currentFileEventNumber;
        private StreamReader _stream;
        private readonly StringBuilder _eventSource;
        private readonly bool _logFileSourcePathIsDirectory;
        private long _eventCount = -1;

        private EventData _currentRow;

        #endregion

        #region Public Properties

        public string CurrentFile
        {
            get
            {
                if (_logFilesWithData.Length <= _indexCurrentFile)
                    return null;

                return _logFilesWithData[_indexCurrentFile];
            }
        }
        public EventData CurrentRow => _currentRow;

        #endregion

        #region Constructor

        internal TechJournalReader()
        { }
        internal TechJournalReader(string logFilePath)
        {
            _eventSource = new StringBuilder();

            if (File.GetAttributes(logFilePath).HasFlag(FileAttributes.Directory))
            {
                _logFileDirectoryPath = logFilePath;
                _logFileSourcePathIsDirectory = true;
                UpdateEventLogFilesFromDirectory();
            }
            else
            {
                _logFileSourcePathIsDirectory = false;
                _logFilesWithData = new[] { logFilePath };
                _logFileDirectoryPath = new FileInfo(_logFilesWithData[0]).Directory?.FullName;
            }
        }

        #endregion

        #region Public Methods

        public bool Read()
        {
            bool output;

            try
            {
                if (!InitializeReadFileStream())
                    return false;

                RaiseBeforeReadFileEvent(out bool cancelBeforeReadFile);
                if (cancelBeforeReadFile)
                {
                    NextFile();
                    return Read();
                }

                while (true)
                {
                    string sourceData = ReadSourceDataFromStream();
                    if (sourceData != null)
                        AddNewLineToSource(sourceData, true);

                    if (LogParserTechJournal.ItsEndOfEvent(_stream, sourceData) || sourceData == null)
                    {
                        _currentFileEventNumber += 1;
                        string preparedSourceData = _eventSource.ToString();
                        _eventSource.Clear();

                        RaiseBeforeRead(new BeforeReadEventArgs(preparedSourceData, _currentFileEventNumber));

                        if (sourceData == null && preparedSourceData.Length == 0)
                        {
                            NextFile();
                            output = Read();
                            break;
                        }

                        try
                        {
                            EventData eventData = ReadRowData(preparedSourceData);
                            _currentRow = eventData;
                            RaiseAfterRead(new AfterReadEventArgs(_currentRow, _currentFileEventNumber));
                            output = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            RaiseOnError(new OnErrorEventArgs(ex, preparedSourceData, false));
                            _currentRow = null;
                            output = true;
                            break;
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                output = false;
            }
            catch (Exception ex)
            {
                RaiseOnError(new OnErrorEventArgs(ex, null, true));
                _currentRow = null;
                output = false;
            }

            return output;
        }
        public bool GoToEvent(long eventNumber)
        {
            Reset();

            int fileIndex = -1;
            long currentLineNumber = -1;
            long currentEventNumber = 0;
            bool moved = false;

            foreach (string logFile in _logFilesWithData)
            {
                fileIndex += 1;
                currentLineNumber = -1;

                IEnumerable<string> allLines = File.ReadLines(logFile);
                foreach (string line in allLines)
                {
                    currentLineNumber += 1;
                    if(LogParserTechJournal.ItsBeginOfEvent(line))
                        currentEventNumber += 1;

                    if (currentEventNumber == eventNumber)
                    {
                        moved = true;
                        break;
                    }
                }

                if (currentEventNumber == eventNumber)
                {
                    moved = true;
                    break;
                }
            }

            if (moved && fileIndex >= 0 && currentLineNumber >= 0)
            {
                InitializeStream(currentLineNumber, fileIndex);
                _eventCount = eventNumber - 1;
                _currentFileEventNumber = eventNumber;

                return true;
            }
            else
            {
                return false;
            }
        }
        public TechJournalPosition GetCurrentPosition()
        {
            return new TechJournalPosition(
                _currentFileEventNumber,
                CurrentFile,
                GetCurrentFileStreamPosition());
        }
        public void SetCurrentPosition(TechJournalPosition newPosition)
        {
            if (ApplyEventLogPosition(newPosition) == false)
                return;

            InitializeStream(0, _indexCurrentFile);
            long beginReadPosition = _stream.GetPosition();
            long newStreamPosition = Math.Max(beginReadPosition, newPosition.StreamPosition ?? 0);

            long sourceStreamPosition = newStreamPosition;
            string currentFilePath = _logFilesWithData[_indexCurrentFile];

            FixEventPosition(currentFilePath, ref newStreamPosition, sourceStreamPosition);

            if (newPosition.StreamPosition != null)
                SetCurrentFileStreamPosition(newStreamPosition);
        }
        public long Count()
        {
            if (_eventCount < 0)
                _eventCount = GetEventCount();

            return _eventCount;
        }
        public void Reset()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            _indexCurrentFile = 0;
            UpdateEventLogFilesFromDirectory();
            _currentFileEventNumber = 0;
            _currentRow = null;
        }
        public void NextFile()
        {
            RaiseAfterReadFile(new AfterReadFileEventArgs(CurrentFile));

            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            _indexCurrentFile += 1;
        }
        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        #endregion

        #region Private Memthods

        private EventData ReadRowData(string sourceData)
        {
            EventData eventData = LogParserTechJournal.Parse(sourceData, CurrentFile);
            return eventData;
        }
        private void AddNewLineToSource(string sourceData, bool newLine)
        {
            if (newLine)
                _eventSource.Append(sourceData);
            else
            {
                _eventSource.AppendLine();
                _eventSource.Append(sourceData);
            }
        }
        private string ReadSourceDataFromStream()
        {
            string sourceData = _stream.ReadLineWithoutNull();
            
            return sourceData;
        }
        private void FindNearestBeginEventPosition(ref bool isCorrectBeginEvent, string currentFilePath, ref long newStreamPosition, int stepSize = 1)
        {
            int attemptToFoundBeginEventLine = 0;
            while (!isCorrectBeginEvent && attemptToFoundBeginEventLine < 10)
            {
                string beginEventLine;
                using (FileStream fileStreamCheckPosition =
                    new FileStream(currentFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStreamCheckPosition.Seek(newStreamPosition, SeekOrigin.Begin);
                    using (StreamReader fileStreamCheckReader = new StreamReader(fileStreamCheckPosition))
                        beginEventLine = fileStreamCheckReader.ReadLineWithoutNull();
                }

                if (beginEventLine == null)
                {
                    isCorrectBeginEvent = false;
                    break;
                }

                isCorrectBeginEvent = LogParserTechJournal.ItsBeginOfEvent(beginEventLine);
                if (!isCorrectBeginEvent)
                {
                    newStreamPosition -= stepSize;
                    attemptToFoundBeginEventLine += 1;
                }
            }
        }
        private void FixEventPosition(string currentFilePath, ref long newStreamPosition, long sourceStreamPosition)
        {
            bool isCorrectBeginEvent = false;

            FindNearestBeginEventPosition(
                ref isCorrectBeginEvent,
                currentFilePath,
                ref newStreamPosition);

            if (!isCorrectBeginEvent)
            {
                newStreamPosition = sourceStreamPosition;
                FindNearestBeginEventPosition(
                    ref isCorrectBeginEvent,
                    currentFilePath,
                    ref newStreamPosition,
                    -1);
            }
        }
        private bool ApplyEventLogPosition(TechJournalPosition position)
        {
            Reset();

            if (position == null)
                return false;

            int indexOfFileData = Array.IndexOf(_logFilesWithData, position.CurrentFileData);
            if (indexOfFileData < 0)
                throw new Exception("Invalid data file");

            _indexCurrentFile = indexOfFileData;
            _currentFileEventNumber = position.EventNumber;

            return true;
        }
        private void SetCurrentFileStreamPosition(long position)
        {
            _stream?.SetPosition(position);
        }
        private long GetCurrentFileStreamPosition()
        {
            return _stream?.GetPosition() ?? 0;
        }
        private long GetEventCount()
        {
            long eventCount = 0;

            foreach (var logFile in _logFilesWithData)
            {
                using (StreamReader logFileStream = new StreamReader(File.Open(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    do
                    {
                        string logFileCurrentString = logFileStream.ReadLineWithoutNull();
                        if (LogParserTechJournal.ItsBeginOfEvent(logFileCurrentString))
                            eventCount++;
                    } while (!logFileStream.EndOfStream);
                }
            }

            return eventCount;
        }
        private void RaiseBeforeReadFileEvent(out bool cancel)
        {
            BeforeReadFileEventArgs beforeReadFileArgs = new BeforeReadFileEventArgs(CurrentFile);
            if (_currentFileEventNumber == 0)
                RaiseBeforeReadFile(beforeReadFileArgs);

            cancel = beforeReadFileArgs.Cancel;
        }
        private void UpdateEventLogFilesFromDirectory()
        {
            if(_logFileSourcePathIsDirectory)
            {
                _logFilesWithData = Directory
                    .GetFiles(_logFileDirectoryPath, "*.log")
                    .OrderBy(i => i)
                    .ToArray();
            }
        }
        private bool InitializeReadFileStream()
        {
            if (_stream == null)
            {
                if (_logFilesWithData.Length <= _indexCurrentFile)
                {
                    _currentRow = null;
                    return false;
                }

                InitializeStream(0, _indexCurrentFile);
                _currentFileEventNumber = 0;
            }
            _eventSource?.Clear();

            return true;
        }
        private void InitializeStream(long linesToSkip, int fileIndex = 0)
        {
            FileStream fs = new FileStream(_logFilesWithData[fileIndex], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _stream = new StreamReader(fs);
            _stream.SkipLine(linesToSkip);
        }

        #endregion

        #region Events

        public delegate void BeforeReadFileHandler(TechJournalReader sender, BeforeReadFileEventArgs args);
        public delegate void AfterReadFileHandler(TechJournalReader sender, AfterReadFileEventArgs args);
        public delegate void BeforeReadEventHandler(TechJournalReader sender, BeforeReadEventArgs args);
        public delegate void AfterReadEventHandler(TechJournalReader sender, AfterReadEventArgs args);
        public delegate void OnErrorEventHandler(TechJournalReader sender, OnErrorEventArgs args);

        public event BeforeReadFileHandler BeforeReadFile;
        public event AfterReadFileHandler AfterReadFile;
        public event BeforeReadEventHandler BeforeReadEvent;
        public event AfterReadEventHandler AfterReadEvent;
        public event OnErrorEventHandler OnErrorEvent;

        protected void RaiseBeforeReadFile(BeforeReadFileEventArgs args)
        {
            BeforeReadFile?.Invoke(this, args);
        }
        protected void RaiseAfterReadFile(AfterReadFileEventArgs args)
        {
            AfterReadFile?.Invoke(this, args);
        }
        protected void RaiseBeforeRead(BeforeReadEventArgs args)
        {
            BeforeReadEvent?.Invoke(this, args);
        }
        protected void RaiseAfterRead(AfterReadEventArgs args)
        {
            AfterReadEvent?.Invoke(this, args);
        }
        protected void RaiseOnError(OnErrorEventArgs args)
        {
            OnErrorEvent?.Invoke(this, args);
        }

        #endregion
    }
}
