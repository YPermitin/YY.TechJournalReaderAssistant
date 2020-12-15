using System.Collections.Generic;
using System.IO;
using System.Linq;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant
{
    public class TechJournalManager
    {
        public List<TechJournalDirectory> Directories { set; get; }

        public TechJournalManager(string logFilePath)
        {
            Directories = new List<TechJournalDirectory>();

            if (File.GetAttributes(logFilePath).HasFlag(FileAttributes.Directory))
            {
                DirectoryInfo logDirectoryInfo = new DirectoryInfo(logFilePath);
                if (TechJournalDirectory.ParseDataFromDirectoryName(logDirectoryInfo.Name)
                    && TechJournalDirectory.ContainsLogFiles(logFilePath))
                {
                    Directories.Add(new TechJournalDirectory(logFilePath));
                }
                else
                {
                    string[] techJournalSubDirectories = Directory.GetDirectories(logFilePath)
                        .OrderBy(i => i)
                        .ToArray();
                    foreach (var techJournalSubDirectory in techJournalSubDirectories)
                    {
                        DirectoryInfo techJournalSubDirectoryInfo = new DirectoryInfo(techJournalSubDirectory);
                        if (TechJournalDirectory.ParseDataFromDirectoryName(techJournalSubDirectoryInfo.Name)
                            && TechJournalDirectory.ContainsLogFiles(techJournalSubDirectoryInfo.FullName))
                        {
                            Directories.Add(new TechJournalDirectory(techJournalSubDirectoryInfo.FullName));
                        }
                    }
                }
            }
            else
            {
                FileInfo logFile = new FileInfo(logFilePath);
                DirectoryInfo logDirectoryInfo = logFile.Directory;
                if (logDirectoryInfo != null
                    && TechJournalDirectory.ParseDataFromDirectoryName(logDirectoryInfo.Name)
                    && TechJournalDirectory.ContainsLogFiles(logDirectoryInfo.FullName))
                {
                    Directories.Add(new TechJournalDirectory(logFilePath));
                }
            }
        }
    }
}
