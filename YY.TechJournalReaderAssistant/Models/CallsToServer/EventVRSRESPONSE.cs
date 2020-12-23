using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.CallsToServer
{
    public class EventVRSRESPONSE : EventData
    {
        public long? Body => Properties.GetLongValueByKey("BODY");
        public string BodyText => Properties.GetStringValueByKey("BODYTEXT");
        public string Headers => Properties.GetStringValueByKey("HEADERS");
        public string Phrase => Properties.GetStringValueByKey("PHRASE");
        public string Status => Properties.GetStringValueByKey("STATUS");
    }
}