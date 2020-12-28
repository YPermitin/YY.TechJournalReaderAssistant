using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Certificates
{
    public class EventMACCERT : EventData
    {
        public string Certificate => Properties.GetStringValueByKey("CERTIFICATE");
        public string ErrorCode => Properties.GetStringValueByKey("ERRORCODE");
    }
}