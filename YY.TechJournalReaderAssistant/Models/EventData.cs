using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.DBMS;

namespace YY.TechJournalReaderAssistant.Models
{
    public class EventData
    {
        #region Private Static Members

        protected static readonly List<string> _commonProperties = new List<string>()
        {
            "p:processName",
            "process",
            "t:applicationName",
            "t:computerName",
            "t:connectID",
            "Context",
            "Usr",
            "func",
            "Database",
            "DBCopy",
            "dbms",
            "dbpid"
        };

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
        public string ServerContextName => Properties.ContainsKey("p:processName") ? Properties["p:processName"] : null;
        public string ProcessName => Properties.ContainsKey("process") ? Properties["process"] : null;
        public int? SessionId
        {
            get
            {
                if (Properties.ContainsKey("SessionID") 
                    && int.TryParse(Properties["SessionID"], out var sessionIdValue))
                    return sessionIdValue;

                return null;
            }
        }
        public string ApplicationName => Properties.ContainsKey("t:applicationName") ? Properties["t:applicationName"] : null;
        public int? ClientId
        {
            get
            {
                if (Properties.ContainsKey("t:clientID") 
                    && int.TryParse(Properties["t:clientID"], out var clientIdValue))
                    return clientIdValue;

                return null;
            }
        }
        public string ComputerName => Properties.ContainsKey("t:computerName") ? Properties["t:computerName"] : null;
        public int? ConnectionId
        {
            get
            {
                if (Properties.ContainsKey("t:connectID") 
                    && int.TryParse(Properties["t:connectID"], out var connectionIdValue))
                    return connectionIdValue;

                return null;
            }
        }
        public string UserName => Properties.ContainsKey("Usr") ? Properties["Usr"] : null;
        public int? ApplicationId
        {
            get
            {
                if (Properties.ContainsKey("AppID") 
                    && int.TryParse(Properties["AppID"], out var applicationIdValue))
                    return applicationIdValue;

                return null;
            }
        }
        public string Context => Properties.ContainsKey("Context") ? Properties["Context"] : null;
        public string ActionTypeValue => Properties.ContainsKey("func") ? Properties["func"] : null;
        public TechJournalAction ActionType => TechJournalActionExtensions.Parse(ActionTypeValue);
        public string Database => Properties.ContainsKey("Database") ? Properties["Database"] : null;
        public string DatabaseCopy => Properties.ContainsKey("DBCopy") ? Properties["DBCopy"] : null;
        public string DBMSValue => Properties.ContainsKey("dbms") ? Properties["dbms"] : null;
        public TechJournalDBMS DBMS => TechJournalDBMSExtensions.Parse(DBMSValue);
        public string DatabasePID => Properties.ContainsKey("dbpid") ? Properties["dbpid"] : null;
        public Dictionary<string, string> Properties { set; get; }

        #endregion

        #region Public Methods

        public string GetCustomFieldsJSON()
        {
            var propertiesForJson = Properties
                .Where(kvp => !_commonProperties.Contains(kvp.Key))
                .Select(kvp => kvp)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            string propertiesAsJson = JsonConvert.SerializeObject(propertiesForJson);

            return propertiesAsJson;
        }

        #endregion
    }
}
