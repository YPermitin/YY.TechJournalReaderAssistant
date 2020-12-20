using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventSESN : EventData
    {
        public string InfobaseName => Properties.GetStringValueByKey("IB");
        public long? NumberOfSession => Properties.GetLongValueByKey("NMB");
    }
}