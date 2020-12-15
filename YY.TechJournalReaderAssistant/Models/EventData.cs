using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models
{
    public class EventData
    {

        public EventData()
        {
            Properties = new Dictionary<string, string>();
        }

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

        public Dictionary<string, string> Properties { set; get; }
    }
}
