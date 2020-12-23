using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.CallsAndContext
{
    public class EventSCALL : EventData
    {
        public string InterfaceName => Properties.GetStringValueByKey("INAME");
        public string Method => Properties.GetStringValueByKey("METHOD");
        public string MethodName => Properties.GetStringValueByKey("MNAME");
    }
}