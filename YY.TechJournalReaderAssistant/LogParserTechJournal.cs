using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models;
using YY.TechJournalReaderAssistant.Models.CallsAndContext;
using YY.TechJournalReaderAssistant.Models.CallsToServer;
using YY.TechJournalReaderAssistant.Models.Certificates;
using YY.TechJournalReaderAssistant.Models.ClusterAndServer;
using YY.TechJournalReaderAssistant.Models.Configuration;
using YY.TechJournalReaderAssistant.Models.DataHistory;
using YY.TechJournalReaderAssistant.Models.DBCopy;
using YY.TechJournalReaderAssistant.Models.DBMS;
using YY.TechJournalReaderAssistant.Models.Errors;
using YY.TechJournalReaderAssistant.Models.ExternalDataSource;
using YY.TechJournalReaderAssistant.Models.FullText;
using YY.TechJournalReaderAssistant.Models.IntegrationService;
using YY.TechJournalReaderAssistant.Models.InteractiveActions;
using YY.TechJournalReaderAssistant.Models.Licensing;
using YY.TechJournalReaderAssistant.Models.Mail;
using YY.TechJournalReaderAssistant.Models.ManagedLocks;
using YY.TechJournalReaderAssistant.Models.MemoryLeaks;
using YY.TechJournalReaderAssistant.Models.SessionData;
using YY.TechJournalReaderAssistant.Models.System;

namespace YY.TechJournalReaderAssistant
{
    internal static class LogParserTechJournal
    {
        #region Private Static Methods

        private static readonly Dictionary<string, Type> _eventObjectTypes = new Dictionary<string, Type>()
        {
            { "DBMSSQLCONN", typeof(EventDBMSSQLCONN) },
            { "DB2", typeof(EventDB2) },
            { "DBMSSQL", typeof(EventDBMSSQL) },
            { "DBORACLE", typeof(EventDBORACLE) },
            { "DBPOSTGRS", typeof(EventDBPOSTGRS) },
            { "DBV8DBEng", typeof(EventDBV8DBEng) },
            { "ADMIN", typeof(EventADMIN) },
            { "CLSTR", typeof(EventCLSTR) },
            { "TLOCK", typeof(EventTLOCK) },
            { "INPUTBYSTRING", typeof(EventINPUTBYSTRING) },
            { "TDEADLOCK", typeof(EventTDEADLOCK) },
            { "EDS", typeof(EventEDS) },
            { "CALL", typeof(EventCALL) },
            { "CONFLOADFROMFILES", typeof(EventCONFLOADFROMFILES) },
            { "SDBL", typeof(EventSDBL) },
            { "VRSREQUEST", typeof(EventVRSREQUEST) },
            { "EXCP", typeof(EventEXCP) },
            { "DHIST", typeof(EventDHIST) },
            { "SCALL", typeof(EventSCALL) },
            { "VRSCACHE", typeof(EventVRSCACHE) },
            { "EXCPCNTX", typeof(EventEXCPCNTX) },
            { "DBCOPIES", typeof(EventDBCOPIES) },
            { "FTEXTCHECK", typeof(EventFTEXTCheck) },
            { "ATTN", typeof(EventATTN) },
            { "FTEXTSKIP", typeof(EventFTEXTSkip) },
            { "FTEXTUPD", typeof(EventFTEXTUpd) },
            { "HASP", typeof(EventHASP) },
            { "MACCERT", typeof(EventMACCERT) },
            { "WINCERT", typeof(EventWINCERT) },
            { "VRSRESPONSE", typeof(EventVRSRESPONSE) },
            { "SDGC", typeof(EventSDGC) },
            { "QERR", typeof(EventQERR) },
            { "MAILPARSEERR", typeof(EventMAILPARSEERR) },
            { "PROC", typeof(EventPROC) },
            { "SESN", typeof(EventSESN) },
            { "SCOM", typeof(EventSCOM) },
            { "SINTEG", typeof(EventSINTEG) },
            { "SRVC", typeof(EventSRVC) },
            { "LIC", typeof(EventLIC) },
            { "SYSTEM", typeof(EventSystem) },
            { "CONN", typeof(EventCONN) },
            { "TTIMEOUT", typeof(EventTTIMEOUT) },
            { "LEAKS", typeof(EventLEAKS) },
            { "MEM", typeof(EventMEM) },
            { "SCRIPTCIRCREFS", typeof(EventSCRIPTCIRCREFS) }
        };

        #endregion

        #region Public Static Methods

        public static bool ItsBeginOfEvent(string sourceString)
        {
            if (sourceString == null)
                return false;

            return Regex.IsMatch(sourceString, @"^\d\d:\d\d.\d\d\d\d(\d\d)?-");
        }
        public static bool ItsEndOfEvent(StreamReader stream, string currentFile, out string outputString)
        {
            if (currentFile == null || stream == null)
            {
                outputString = null;
                return true;
            }

            outputString = stream.ReadLineWithoutNull();
            return ItsBeginOfEvent(outputString) || outputString == null;
        }
        public static EventData Parse(string originEventSource, string currentFile, long eventId, TimeZoneInfo timeZone)
        {
            string bufferEventSource = String.Copy(originEventSource);
            FileInfo currentFileInfo = new FileInfo(currentFile);
            string dateFromFileAsString = currentFileInfo.Name.Replace(".log", string.Empty);

            int indexEndOfDate = bufferEventSource.IndexOf('-');
            string periodAsString = bufferEventSource.Substring(0, indexEndOfDate);
            long periodMilliseconds = long.Parse(periodAsString.Substring(6, 3));

            DateTime eventPeriod = new DateTime(
                2000 + int.Parse(dateFromFileAsString.Substring(0, 2)),
                int.Parse(dateFromFileAsString.Substring(2, 2)),
                int.Parse(dateFromFileAsString.Substring(4, 2)),
                int.Parse(dateFromFileAsString.Substring(6, 2)),
                int.Parse(periodAsString.Substring(0, 2)),
                int.Parse(periodAsString.Substring(3, 2))
            ).AddMilliseconds(periodMilliseconds);

            long periodMoment;
            bool isFormat83 = periodAsString.Length == 12;
            if (isFormat83)
                periodMoment = long.Parse(periodAsString.Substring(6, 6));
            else
                periodMoment = long.Parse(periodAsString.Substring(6, 4)) * 100;

            int indexEndOfDuration = bufferEventSource.IndexOf(',');
            string durationAsString = bufferEventSource.Substring(indexEndOfDate + 1, indexEndOfDuration - indexEndOfDate - 1);
            long duration = long.Parse(durationAsString) * (isFormat83 ? 10 : 100);

            bufferEventSource = bufferEventSource.Substring(indexEndOfDuration + 1, bufferEventSource.Length - indexEndOfDuration - 1);
            int indexEndOfEventName = bufferEventSource.IndexOf(',');
            string eventName = bufferEventSource.Substring(0, indexEndOfEventName);

            string eventNameKey = eventName.ToUpper();
            if (!_eventObjectTypes.TryGetValue(eventNameKey, out var eventObjectType))
                eventObjectType = typeof(EventData);
            EventData dataRow = (EventData)Activator.CreateInstance(eventObjectType);
            dataRow.Id = eventId;

            dataRow.Period = eventPeriod;
            dataRow.PeriodUTC = TimeZoneInfo.ConvertTimeToUtc(dataRow.Period, timeZone ?? TimeZoneInfo.Local);
            dataRow.PeriodMoment = periodMoment;
            dataRow.Duration = duration;
            dataRow.EventName = eventName;

            bufferEventSource = bufferEventSource.Substring(indexEndOfEventName + 1, bufferEventSource.Length - indexEndOfEventName - 1);
            int indexEndOfLevel = bufferEventSource.IndexOf(',');
            dataRow.Level = int.Parse(bufferEventSource.Substring(0, indexEndOfLevel));

            bufferEventSource = bufferEventSource.Substring(indexEndOfLevel + 1, bufferEventSource.Length - indexEndOfLevel - 1);
            int indexOfDelimiter = bufferEventSource.IndexOf("=", StringComparison.InvariantCulture);

            bufferEventSource = bufferEventSource.Replace("''", "¦");
            bufferEventSource = bufferEventSource.Replace(@"""""", "÷");

            while (indexOfDelimiter > 0)
            {
                string paramName = bufferEventSource
                    .Substring(0, indexOfDelimiter)
                    .ToUpper()
                    .Trim();
                string valueAsString = string.Empty;

                bufferEventSource = bufferEventSource.Substring(indexOfDelimiter + 1);
                if (!string.IsNullOrEmpty(bufferEventSource))
                {
                    if (bufferEventSource.Substring(0, 1) == "'")
                    {
                        bufferEventSource = bufferEventSource.Substring(1);
                        indexOfDelimiter = bufferEventSource.IndexOf("'", StringComparison.InvariantCulture);
                        if (indexOfDelimiter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimiter).Trim();
                            valueAsString = valueAsString.Replace("¦", "'");
                        }
                        if (bufferEventSource.Length > indexOfDelimiter + 1)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimiter + 1 + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                    else if (bufferEventSource.Substring(0, 1) == "\"")
                    {
                        bufferEventSource = bufferEventSource.Substring(1);
                        indexOfDelimiter = bufferEventSource.IndexOf("\"", StringComparison.InvariantCulture);
                        if (indexOfDelimiter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimiter).Trim();
                            valueAsString = valueAsString.Replace("÷", "\"\"");
                        }
                        if (bufferEventSource.Length > indexOfDelimiter + 1)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimiter + 1 + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                    else
                    {
                        indexOfDelimiter = bufferEventSource.IndexOf(",", StringComparison.Ordinal);
                        if (indexOfDelimiter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimiter).Trim();
                        }
                        else
                        {
                            valueAsString = bufferEventSource;
                        }

                        if (bufferEventSource.Length > indexOfDelimiter)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimiter + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                }

                indexOfDelimiter = bufferEventSource.IndexOf("=", StringComparison.InvariantCulture);

                if (dataRow.Properties.ContainsKey(paramName))
                {
                    int countParamWithSameName = dataRow.Properties.Count(e => e.Key.StartsWith(paramName));
                    dataRow.Properties.Add($"{paramName}#{countParamWithSameName + 1}", valueAsString);
                }
                else
                {
                    dataRow.Properties.Add(paramName, valueAsString);
                }
            }

            return dataRow;
        }

        #endregion
    }
}
