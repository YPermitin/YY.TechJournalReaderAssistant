using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DataHistory
{
    public class EventDHIST : EventData
    {
        public string DataHistoryDescription => Properties.GetStringValueByKey("DESCRIPTION");
    }
}