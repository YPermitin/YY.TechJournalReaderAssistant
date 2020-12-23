using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DataHistory
{
    public class EventDHIST : EventData
    {
        public string Description => Properties.GetStringValueByKey("DESCRIPTION");
    }
}