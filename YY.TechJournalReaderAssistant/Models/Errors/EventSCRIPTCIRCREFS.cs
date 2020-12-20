using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventMAILPARSEERR : EventData
    {
        public string Cycles => Properties.GetStringValueByKey("CYCLES");
        public string ModuleName => Properties.GetStringValueByKey("MODULENAME");
    }
}