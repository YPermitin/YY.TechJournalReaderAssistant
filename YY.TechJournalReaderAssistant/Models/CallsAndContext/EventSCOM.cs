using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.CallsAndContext
{
    public class EventSCOM : EventData
    {
        public string InterfaceName => Properties.GetStringValueByKey("INAME");
        public string SrcProcessName => Properties.GetStringValueByKey("SRCPROCESSNAME");
    }
}