using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ManagedLocks
{
    public class EventTLOCK : EventData
    {
        public string Error => Properties.GetStringValueByKey("ERROR");
        public string Exception => Properties.GetStringValueByKey("EXCEPTION");
        public string Locks => Properties.GetStringValueByKey("LOCKS");
        public string Regions => Properties.GetStringValueByKey("REGIONS");
        public string WaitConnections => Properties.GetStringValueByKey("WAITCONNECTIONS");
    }
}