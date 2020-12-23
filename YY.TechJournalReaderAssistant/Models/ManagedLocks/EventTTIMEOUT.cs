using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ManagedLocks
{
    public class EventTTIMEOUT : EventData
    {
        public string WaitConnections => Properties.GetStringValueByKey("WAITCONNECTIONS");
    }
}