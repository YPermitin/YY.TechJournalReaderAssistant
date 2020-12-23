using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.CallsToServer
{
    public class EventVRSCACHE : EventData
    {
        public string Method => Properties.GetStringValueByKey("METHOD");
        public string SQL => Properties.GetStringValueByKey("SQL");
    }
}