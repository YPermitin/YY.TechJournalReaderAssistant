using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System.IO.Compression;
using System.Linq;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant.Tests
{
    public class TechJournalReaderTests
    {
        [Fact]
        public void Test_ReadSampleData()
        {
            string unitTestDirectory = Directory.GetCurrentDirectory();

            string logArchive = Path.Combine(unitTestDirectory, "TestData", "TestData_ServerAndClusterLogs.zip");
            string logDataPath = Path.Combine(unitTestDirectory, "TestData", "TestData_ServerAndClusterLogs");
            if(Directory.Exists(logDataPath)) Directory.Delete(logDataPath, true);
            ZipFile.ExtractToDirectory(logArchive, logDataPath);

            int eventNumber = 0;
            EventData lastRow = null;
            List<EventData> allRows = new List<EventData>();

            TechJournalManager tjManager = new TechJournalManager(logDataPath);
            foreach (var tjDirectory in tjManager.Directories)
            {
                TechJournalReader tjReader = TechJournalReader.CreateReader(tjDirectory.DirectoryData.FullName);
                while (tjReader.Read())
                {
                    eventNumber += 1;
                    lastRow = tjReader.CurrentRow;
                    allRows.Add(tjReader.CurrentRow);
                }
            }

            DateTime minPeriod = allRows.Min(r => r.Period);
            DateTime maxPeriod = allRows.Max(r => r.Period);

            Assert.NotNull(lastRow);
            Assert.NotEqual(0, eventNumber);
            Assert.Equal(3113, eventNumber);
            Assert.Equal(new DateTime(2020, 8, 18, 15, 22, 06).AddMilliseconds(356), minPeriod);
            Assert.Equal(new DateTime(2020, 8, 18, 16, 02, 34).AddMilliseconds(817), maxPeriod);
        }
    }
}
