using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.FullText
{
    public class EventFTEXTSkip : EventData
    {
        public string Attribute => Properties.GetStringValueByKey("ATTRIBUTE");
        public long? Size => Properties.GetLongValueByKey("SIZE");
        public string URL => Properties.GetStringValueByKey("URL");
    }
}