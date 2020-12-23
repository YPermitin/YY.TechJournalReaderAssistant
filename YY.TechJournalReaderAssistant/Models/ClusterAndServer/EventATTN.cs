using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventATTN : EventData
    {
        public string ActionDescription => Properties.GetStringValueByKey("ACTION");
        public string AgentURL => Properties.GetStringValueByKey("AGENTURL");
        public long? AvgExceptions => Properties.GetLongValueByKey("AVGEXCEPTIONS");
        public long? Attempts => Properties.GetLongValueByKey("ATTEMPTS");
        public string Cause => Properties.GetStringValueByKey("CAUSE");
        public long? CurrentExceptions => Properties.GetLongValueByKey("CUREXCEPTIONS");
        public string ExceptionDescription => Properties.ContainsKey("descr") ? Properties["descr"] : null;
        public string Host => Properties.GetStringValueByKey("HOST");
        public string LimitName => Properties.GetStringValueByKey("LIMITNAME");
        public long? MaxMemSize => Properties.GetLongValueByKey("MAXMEMSIZE");
        public long? MemSize => Properties.GetLongValueByKey("MEMSIZE");
        public long? OSThread => Properties.GetLongValueByKey("OSTHREAD");
        public long? PID => Properties.GetLongValueByKey("PID");
        public string ProcessURL => Properties.GetStringValueByKey("PROCURL");
        public string SeanceID => Properties.GetStringValueByKey("SEANCEID");
        public string TimeoutProcess => Properties.GetStringValueByKey("TIMEOUT");
        public string MessageTypeValue => Properties.GetStringValueByKey("TYPE");
        public TechJournalClusterMessageType MessageType => TechJournalClusterMessageTypeExtensions.Parse(MessageTypeValue);
    }
}