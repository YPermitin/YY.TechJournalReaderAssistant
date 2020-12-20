using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventSDBL : EventData
    {
        public long? Rows => Properties.GetLongValueByKey("ROWS");
        public string SDBL => Properties.GetStringValueByKey("SDBL");
    }
}