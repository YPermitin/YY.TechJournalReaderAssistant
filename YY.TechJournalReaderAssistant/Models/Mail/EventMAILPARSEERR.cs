using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Mail
{
    public class EventMAILPARSEERR : EventData
    {
        public string MethodHTTP => Properties.GetStringValueByKey("METHOD");
        public string MessageUid => Properties.GetStringValueByKey("MESSAGEUID");
    }
}