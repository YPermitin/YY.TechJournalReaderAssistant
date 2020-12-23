using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.IntegrationService
{
    public class EventSINTEG : EventData
    {
        public long? BodySize => Properties.GetLongValueByKey("BODYSIZE");
        public string ChannelName => Properties.GetStringValueByKey("CHANNELNAME");
        public string ExtSrvcURL => Properties.GetStringValueByKey("EXTSRVCURL");
        public string ExtSrvcUsr => Properties.GetStringValueByKey("EXTSRVCUSR");
        public string MessageId => Properties.GetStringValueByKey("MESSAGEID");
        public string SrvcName => Properties.GetStringValueByKey("SRVCNAME");


    }
}