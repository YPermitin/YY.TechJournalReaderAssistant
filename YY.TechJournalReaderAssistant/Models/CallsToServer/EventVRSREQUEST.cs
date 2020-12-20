using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.CallsToServer
{
    public class EventVRSREQUEST : EventData
    {
        public long? Body => Properties.GetLongValueByKey("BODY");
        public string BodyText => Properties.GetStringValueByKey("BODYTEXT");
        public string Headers => Properties.GetStringValueByKey("HEADERS");
        public string Method => Properties.GetStringValueByKey("METHODS");
        public string Resource => Properties.GetStringValueByKey("URI");
    }
}