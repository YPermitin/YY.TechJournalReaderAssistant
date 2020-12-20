using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventSRVC : EventData
    {
        public string ExceprionDescription => Properties.GetStringValueByKey("DESCR");
    }
}