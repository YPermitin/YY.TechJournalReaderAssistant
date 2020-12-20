using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.MemoryLeaks
{
    public class EventMEM : EventData
    {
        public long? MemoryUsed => Properties.GetLongValueByKey("CN");
        public long? MemoryUsedChange => Properties.GetLongValueByKey("CDN");
        public long? ProcessMemoryUsed => Properties.GetLongValueByKey("SZ");
        public long? ProcessMemoryUsedChange => Properties.GetLongValueByKey("SZD");
    }
}