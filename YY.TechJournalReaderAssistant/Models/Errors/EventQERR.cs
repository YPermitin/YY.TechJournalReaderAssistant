using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventQERR : EventData
    {
        public string Query => Properties.GetStringValueByKey("QUERY");
    }
}