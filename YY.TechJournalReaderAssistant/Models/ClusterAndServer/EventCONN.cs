using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventCONN : EventData
    {
        public long? Calls => Properties.GetLongValueByKey("CALLS");
        public string Message => Properties.GetStringValueByKey("TXT");
    }
}