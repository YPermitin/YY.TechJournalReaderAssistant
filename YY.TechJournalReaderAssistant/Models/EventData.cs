using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using YY.TechJournalReaderAssistant.Helpers;
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
using YY.TechJournalReaderAssistant.Models.Special;
using YY.TechJournalReaderAssistant.Models.System;

namespace YY.TechJournalReaderAssistant.Models
{
    public class EventData : IEventData
    {
        #region Private Static Members

        private static Dictionary<string, Type> _eventObjectTypes = new Dictionary<string, Type>()
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
        protected static readonly List<string> _commonProperties = new List<string>()
        {
            "P:PROCESSNAME",
            "PROCESS",
            "T:APPLICATIONNAME",
            "T:COMPUTERNAME",
            "T:CONNECTID",
            "CONTEXT",
            "USR",
            "FUNC",
            "DATABASE",
            "DBCOPY",
            "DBMS",
            "DBPID"
        };

        private static readonly Regex _replaceTempTableName = new Regex(@"#tt[\d]+");
        private static readonly Regex _replaceParameterName = new Regex(@"@P[\d]+");

        private string _sqlQueryOnly;
        private string _sqlQueryParametersOnly;

        #endregion

        #region Public Static Methods

        public static EventData Create(string originEventSource, string currentFile)
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

            Type eventObjectType;
            string eventNameKey = eventName.ToUpper();
            if (!_eventObjectTypes.TryGetValue(eventNameKey, out eventObjectType))
                eventObjectType = typeof(EventData);
            EventData dataRow = (EventData)Activator.CreateInstance(eventObjectType);

            dataRow.Period = eventPeriod;
            dataRow.PeriodMoment = periodMoment;
            dataRow.Duration = duration;
            dataRow.EventName = eventName;

            bufferEventSource = bufferEventSource.Substring(indexEndOfEventName + 1, bufferEventSource.Length - indexEndOfEventName - 1);
            int indexEndOfLevel = bufferEventSource.IndexOf(',');
            dataRow.Level = int.Parse(bufferEventSource.Substring(0, indexEndOfLevel));

            bufferEventSource = bufferEventSource.Substring(indexEndOfLevel + 1, bufferEventSource.Length - indexEndOfLevel - 1);
            int indexOfDelimeter = bufferEventSource.IndexOf("=", StringComparison.InvariantCulture);

            bufferEventSource = bufferEventSource.Replace("''", "¦");
            bufferEventSource = bufferEventSource.Replace(@"""""", "÷");

            while (indexOfDelimeter > 0)
            {
                string paramName = bufferEventSource.Substring(0, indexOfDelimeter).ToUpper();
                string valueAsString = string.Empty;

                bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1);
                if (!string.IsNullOrEmpty(bufferEventSource))
                {
                    if (bufferEventSource.Substring(0, 1) == "'")
                    {
                        bufferEventSource = bufferEventSource.Substring(1);
                        indexOfDelimeter = bufferEventSource.IndexOf("'", StringComparison.InvariantCulture);
                        if (indexOfDelimeter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimeter).Trim();
                            valueAsString = valueAsString.Replace("¦", "'");
                        }
                        if (bufferEventSource.Length > indexOfDelimeter + 1)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1 + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                    else if (bufferEventSource.Substring(0, 1) == "\"")
                    {
                        bufferEventSource = bufferEventSource.Substring(1);
                        indexOfDelimeter = bufferEventSource.IndexOf("\"", StringComparison.InvariantCulture);
                        if (indexOfDelimeter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimeter).Trim();
                            valueAsString = valueAsString.Replace("÷", "\"\"");
                        }
                        if (bufferEventSource.Length > indexOfDelimeter + 1)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1 + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                    else
                    {
                        indexOfDelimeter = bufferEventSource.IndexOf(",", StringComparison.Ordinal);
                        if (indexOfDelimeter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimeter).Trim();
                        }
                        else
                        {
                            valueAsString = bufferEventSource;
                        }

                        if (bufferEventSource.Length > indexOfDelimeter)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                }

                indexOfDelimeter = bufferEventSource.IndexOf("=", StringComparison.InvariantCulture);

                if (dataRow.Properties.ContainsKey(paramName))
                {
                    int countParamWithSameName = dataRow.Properties.Count(e => e.Key == paramName);
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

        #region Constructors

        public EventData()
        {
            Properties = new Dictionary<string, string>();
        }

        #endregion
        
        #region Public Members

        public DateTime Period { set; get; }
        public long PeriodMoment { set; get; }
        public int Level { set; get; }
        public long Duration { set; get; }
        public long DurationSec => Duration / 1000000;
        public string EventName { set; get; }
        public string ServerContextName => Properties.GetStringValueByKey("P:PROCESSNAME");
        public string ProcessName => Properties.GetStringValueByKey("PROCESS");
        public long? SessionId => Properties.GetLongValueByKey("SESSIONID");
        public string ApplicationName => Properties.GetStringValueByKey("T:APPLICATIONNAME");
        public long? ClientId => Properties.GetLongValueByKey("T:CLIENTID");
        public string ComputerName => Properties.GetStringValueByKey("T:COMPUTERNAME");
        public long? ConnectionId => Properties.GetLongValueByKey("T:CONNECTID");
        public string UserName => Properties.GetStringValueByKey("USR");
        public long? ApplicationId => Properties.GetLongValueByKey("APPID");
        public string Context => Properties.GetStringValueByKey("CONTEXT");
        public string ActionTypeValue => Properties.GetStringValueByKey("FUNC");
        public TechJournalAction ActionType => TechJournalActionExtensions.Parse(ActionTypeValue);
        public string Database => Properties.GetStringValueByKey("DATABASE");
        public string DatabaseCopy => Properties.GetStringValueByKey("DBCOPY");
        public string DBMSValue => Properties.GetStringValueByKey("DBMS");
        public TechJournalDBMS DBMS => TechJournalDBMSExtensions.Parse(DBMSValue);
        public string DatabasePID => Properties.GetStringValueByKey("DBPID");
        public string PlanSQLText => Properties.GetStringValueByKey("PLANSQLTEXT");
        public long? Rows => Properties.GetLongValueByKey("ROWS");
        public long? RowsAffected => Properties.GetLongValueByKey("ROWSAFFECTED");
        public string SQLText => Properties.GetStringValueByKey("SQL");
        public string SQLQueryOnly
        {
            get
            {
                if (_sqlQueryOnly == null && Properties.ContainsKey("Sql"))
                {
                    string bufferSql = (string)Properties["Sql"].Clone();
                    int endOfQuery = bufferSql.IndexOf("p_0", StringComparison.Ordinal);
                    _sqlQueryOnly = bufferSql.Substring(0, endOfQuery);
                }

                return _sqlQueryOnly;
            }
        }
        public string SQLQueryParametersOnly
        {
            get
            {
                if (_sqlQueryParametersOnly == null && Properties.ContainsKey("Sql"))
                {
                    string bufferSql = (string)Properties["Sql"].Clone();
                    int endOfQuery = bufferSql.IndexOf("p_0", StringComparison.Ordinal);
                    int lengthOfParams = bufferSql.Length - endOfQuery;
                    _sqlQueryParametersOnly = bufferSql.Substring(endOfQuery, lengthOfParams);
                }

                return _sqlQueryParametersOnly;
            }
        }
        public string SQLQueryHash
        {
            get
            {
                if (!string.IsNullOrEmpty(SQLQueryOnly))
                {
                    string bufferSql = (string)SQLQueryOnly.Clone();
                    _replaceParameterName.Replace(bufferSql, bufferSql);
                    _replaceTempTableName.Replace(bufferSql, bufferSql);
                    bufferSql = bufferSql.Replace(" ", "");
                    return bufferSql.CreateMD5();
                }

                return null;
            }
        }
        public string SDBL => Properties.GetStringValueByKey("SDBL");
        public string Description => Properties.GetStringValueByKey("DESCR");
        public string Message => Properties.GetStringValueByKey("TXT");
        public Dictionary<string, string> Properties { set; get; }

        #endregion

        #region Public Methods

        public string GetCustomFieldsAsJSON()
        {
            var propertiesForJson = Properties
                .Where(kvp => !_commonProperties.Contains(kvp.Key))
                .Select(kvp => kvp)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            string propertiesAsJson = JsonConvert.SerializeObject(propertiesForJson);

            return propertiesAsJson;
        }
        public virtual T AsEvent<T>() where T : EventData, IEventData
        {
            if (this is T)
                return (T) this;
            return null;
        }

        #endregion
    }
}
