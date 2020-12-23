using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ManagedLocks
{
    public class EventTDEADLOCK : EventData
    {
        public string DeadlockConnectionIntersections => Properties.GetStringValueByKey("DEADLOCKCONNECTIONINTERSECTIONS");
    }
}