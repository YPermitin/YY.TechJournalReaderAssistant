using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.FullText
{
    public class EventFTEXTCheck : EventData
    {
        public string File => Properties.GetStringValueByKey("FILE");
        public string Info => Properties.GetStringValueByKey("INFO");
        public long? Result => Properties.GetLongValueByKey("RESULT");
        public string Separation => Properties.GetStringValueByKey("SEPARATION");
        public string SepId => Properties.GetStringValueByKey("SEPID");
        public string Word => Properties.GetStringValueByKey("WORD");
    }
}