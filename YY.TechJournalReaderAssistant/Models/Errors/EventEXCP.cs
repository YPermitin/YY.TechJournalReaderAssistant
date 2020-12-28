using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventEXCP : EventData
    {
        public string DumpError => Properties.GetStringValueByKey("DUMPERROR");
        public string DumpFile => Properties.GetStringValueByKey("DUMPFILE");
        public string Exception => Properties.GetStringValueByKey("EXCEPTION");
        public string OSException => Properties.GetStringValueByKey("OSEXCEPTION");
    }
}