using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models.Configuration
{
    public class EventCONFLOADFROMFILES : EventData
    {
        public string EventSeverityValue => Properties.GetStringValueByKey("LEVEL");
        public TechJournalEventSeverity EventSeverity => TechJournalEventSeverityExtensions.Parse(EventSeverityValue);
    }
}