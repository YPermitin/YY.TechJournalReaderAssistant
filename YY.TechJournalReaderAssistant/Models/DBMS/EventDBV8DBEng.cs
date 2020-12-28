using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBV8DBEng : EventData
    {
        public long? NumberParams => Properties.GetLongValueByKey("NPARAMS");
    }
}