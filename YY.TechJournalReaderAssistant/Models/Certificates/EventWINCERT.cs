using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Certificates
{
    public class EventWINCERT : EventData
    {
        public string Certificate => Properties.GetStringValueByKey("CERTIFICATE");
        public string Description => Properties.GetStringValueByKey("DESCR");
        public string ErrorCode => Properties.GetStringValueByKey("ERRORCODE");
    }
}