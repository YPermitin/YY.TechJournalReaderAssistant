using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models.System
{
    public class EventSystem : EventData
    {
        public string Component => Properties.GetStringValueByKey("COMPONENT");
        public string Class => Properties.GetStringValueByKey("CLASS");
        public string File => Properties.GetStringValueByKey("FILE");
        public string EventSeverityValue => Properties.GetStringValueByKey("LEVEL");
        public TechJournalEventSeverity EventSeverity => TechJournalEventSeverityExtensions.Parse(EventSeverityValue);
    }
}