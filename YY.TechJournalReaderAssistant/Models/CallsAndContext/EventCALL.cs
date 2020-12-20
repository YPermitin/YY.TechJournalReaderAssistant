using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.CallsAndContext
{
    public class EventCALL : EventData
    {
        public long? CpuTime => Properties.GetLongValueByKey("CPUTIME");
        public string FindByString => Properties.GetStringValueByKey("FINDBYSTRING");
        public string InterfaceName => Properties.GetStringValueByKey("INAME");
        public long? InBytes => Properties.GetLongValueByKey("INBYTES");
        public string IntegrationService => Properties.GetStringValueByKey("INTEGRATIONSERVICE");
        public string Method => Properties.GetStringValueByKey("METHOD");
        public long? Memory => Properties.GetLongValueByKey("MEMORY");
        public long? MemoryPeak => Properties.GetLongValueByKey("MEMORYPEAK");
        public string MethodName => Properties.GetStringValueByKey("MNAME");
        public long? OutBytes => Properties.GetLongValueByKey("OUTBYTES");
        public string Report => Properties.GetStringValueByKey("REPORT");
        public string RetExcp => Properties.GetStringValueByKey("RETEXCP");
    }
}