using System;
using System.Collections.Generic;
using System.Linq;
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
        public EventData(EventData data) : base()
        {
            _eventData = data;
        }

        #endregion

        #region Private Members

        protected EventData _eventData;

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
            IEventData eventData = this;
            object[] args = new[]
            {
                this
            };
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        #endregion
    }
}
