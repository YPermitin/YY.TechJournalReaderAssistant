using System;
using System.Collections.Generic;
using System.Threading;
using YY.TechJournalReaderAssistant;
using YY.TechJournalReaderAssistant.EventArguments;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistantConsoleApp
{
    static class Program
    {
        private static int _totalEventNumber;
        private static int _eventNumber;
        private static DateTime _lastPeriodEvent = DateTime.MinValue;
        private static TechJournalDirectory _lastLogDirectory;
        private static Dictionary<string, TechJournalPosition> _lastPositions =
            new Dictionary<string, TechJournalPosition>();

        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;
            
            string dataDirectoryPath = args[0];
            Console.WriteLine($"{DateTime.Now}: Инициализация чтения логов \"{dataDirectoryPath}\"...");
            Console.WriteLine();
            
            while (true)
            {
                TechJournalManager tjManager = new TechJournalManager(dataDirectoryPath);
                foreach (var tjDirectory in tjManager.Directories)
                {
                    if(!tjDirectory.DirectoryData.Exists)
                        continue;
                    
                    _lastLogDirectory = tjDirectory;
                    TechJournalPosition lastPosition = null;
                    if (_lastPositions.ContainsKey(tjDirectory.DirectoryData.Name))
                        lastPosition = _lastPositions[tjDirectory.DirectoryData.Name];

                    using (TechJournalReader tjReader = TechJournalReader.CreateReader(tjDirectory.DirectoryData.FullName))
                    {
                        tjReader.AfterReadEvent += Reader_AfterReadEvent;
                        tjReader.AfterReadFile += Reader_AfterReadFile;
                        tjReader.BeforeReadEvent += Reader_BeforeReadEvent;
                        tjReader.BeforeReadFile += Reader_BeforeReadFile;
                        tjReader.OnErrorEvent += Reader_OnErrorEvent;
                        tjReader.SetCurrentPosition(lastPosition);
                        
                        _eventNumber = 0;
                        while (tjReader.Read())
                        {
                            // tjReader.CurrentRow - данные текущего события
                            // tjReader.CurrentRow.Properties - сырые данные события "как есть" без обработки в виде словаря Dictionary<string, string>
                            _eventNumber += 1;
                            _totalEventNumber += 1;
                        }
                        Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Всего событий прочитано: ({_eventNumber})...");
                    }
                }
                
                Console.WriteLine();
                Console.WriteLine($"{DateTime.Now}: Всего событий прочитано {_totalEventNumber}.");
                Console.WriteLine($"{DateTime.Now}: Ожидание новых событий...");
                Thread.Sleep(5000);
            }
        }

        #region Events

        private static void Reader_BeforeReadFile(TechJournalReader sender, BeforeReadFileEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Начало чтения файла \"{args.FileName}\"");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_eventNumber}");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_lastPeriodEvent}");
        }
        private static void Reader_AfterReadFile(TechJournalReader sender, AfterReadFileEventArgs args)
        {
            TechJournalPosition currentPosition = sender.GetCurrentPosition();
            if (_lastPositions.ContainsKey(_lastLogDirectory.DirectoryData.Name))
                _lastPositions[_lastLogDirectory.DirectoryData.Name] = sender.GetCurrentPosition();
            else
                _lastPositions.Add(_lastLogDirectory.DirectoryData.Name, sender.GetCurrentPosition());
            
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Окончание чтения файла \"{args.FileName}\"");
            Console.WriteLine();
        }
        private static void Reader_BeforeReadEvent(TechJournalReader sender, BeforeReadEventArgs args)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] (+){_eventNumber}");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_lastPeriodEvent}");
        }
        private static void Reader_AfterReadEvent(TechJournalReader sender, AfterReadEventArgs args)
        {
            if (args.EventData != null)
                _lastPeriodEvent = args.EventData.Period;

            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] [+]{_eventNumber}");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_lastPeriodEvent}");
        }
        private static void Reader_OnErrorEvent(TechJournalReader sender, OnErrorEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Ошибка чтения логов \"{args.Exception}\"");
        }

        #endregion
    }
}
