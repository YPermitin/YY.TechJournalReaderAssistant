using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Licensing
{
    public class EventHASP : EventData
    {
        public string Text => Properties.GetStringValueByKey("TXT");
    }
}