using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models.Licensing
{
    public class EventLIC : EventData
    {
        public string StateValue => Properties.GetStringValueByKey("RES");
        public TechJournalLicensingState State => TechJournalLicensingStateExtensions.Parse(StateValue);
    }
}