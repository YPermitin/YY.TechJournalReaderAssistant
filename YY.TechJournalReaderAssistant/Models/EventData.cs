using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models
{
    public class EventData : IEventData
    {
        #region Private Static Members
        
        protected static readonly List<string> _commonProperties = new List<string>()
        {
            "P:PROCESSNAME",
            "PROCESS",
            "T:APPLICATIONNAME",
            "T:COMPUTERNAME",
            "T:CONNECTID",
            "T:CLIENTID",
            "CONTEXT",
            "USR",
            "FUNC",
            "DATABASE",
            "DBCOPY",
            "DBMS",
            "DBPID",
            "PLANSQLTEXT",
            "ROWS",
            "ROWSAFFECTED",
            "SQL",
            "SDBL",
            "DESCR",
            "TXT"
        };

        private static readonly Regex _replaceTempTableName = new Regex(@"#tt[\d]+");
        private static readonly Regex _replaceParameterName = new Regex(@"@P[\d]+");

        private string _sqlQueryOnly;
        private string _sqlQueryParametersOnly;

        #endregion

        #region Public Static Methods

        public static EventData Create(string originEventSource, string currentFile, long eventId)
        {
            return LogParserTechJournal.Parse(originEventSource, currentFile, eventId);
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
        public long Id { set; get; }
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
                if (_sqlQueryOnly == null && Properties.ContainsKey("SQL"))
                {
                    string bufferSql = (string)Properties["SQL"].Clone();
                    int endOfQuery = bufferSql.IndexOf("p_0", StringComparison.Ordinal);
                    if (endOfQuery > 0)
                        _sqlQueryOnly = bufferSql.Substring(0, endOfQuery);
                    else
                        _sqlQueryOnly = bufferSql;
                }

                return _sqlQueryOnly;
            }
        }
        public string SQLQueryParametersOnly
        {
            get
            {
                if (_sqlQueryParametersOnly == null && Properties.ContainsKey("SQL"))
                {
                    string bufferSql = (string)Properties["SQL"].Clone();
                    int endOfQuery = bufferSql.IndexOf("p_0", StringComparison.Ordinal);
                    if (endOfQuery > 0)
                    {
                        int lengthOfParams = bufferSql.Length - endOfQuery;
                        if(lengthOfParams > 0)
                            _sqlQueryParametersOnly = bufferSql.Substring(endOfQuery, lengthOfParams);
                    }
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
