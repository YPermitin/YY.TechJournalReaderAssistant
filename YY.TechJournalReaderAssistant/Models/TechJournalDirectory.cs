using System.IO;

namespace YY.TechJournalReaderAssistant.Models
{
    public class TechJournalDirectory
    {
        #region Public Static Methods

        public static bool ContainsLogFiles(string directoryName)
        {
            string[] logFiles = Directory.GetFiles(directoryName, "*.log");
            return logFiles.Length > 0;
        }
        public static bool ParseDataFromDirectoryName(string directoryName)
        {
            return ParseDataFromDirectoryName(directoryName, out _, out _);
        }
        public static bool ParseDataFromDirectoryName(string directoryName, out string processName, out int? processId)
        {
            string[] directoryNameParts = directoryName.Split('_');
            if (directoryNameParts.Length == 2
                && int.TryParse(directoryNameParts[1], out var processIdValue))
            {
                processName = directoryNameParts[0];
                processId = processIdValue;
                return true;
            }
            else
            {
                processName = null;
                processId = null;
                return false;
            }
        }

        #endregion

        #region Constructors

        public TechJournalDirectory(string directoryPath)
        {
            if (File.GetAttributes(directoryPath).HasFlag(FileAttributes.Directory))
                DirectoryData = new DirectoryInfo(directoryPath);
            else
            {
                FileInfo logFile = new FileInfo(directoryPath);
                DirectoryData = logFile.Directory;
            }

            UpdateProcessInfoByDirectory();
        }

        #endregion

        #region Public Members

        public DirectoryInfo DirectoryData { private set; get; }
        public string ProcessName { private set; get; }
        public int? ProcessId { private set; get; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return DirectoryData.Name;
        }

        #endregion

        #region Private Methods

        private void UpdateProcessInfoByDirectory()
        {
            ParseDataFromDirectoryName(DirectoryData.Name, out var processNameValue, out var processIdValue);
            ProcessName = processNameValue;
            ProcessId = processIdValue;
        }
        
        #endregion
    }
}
