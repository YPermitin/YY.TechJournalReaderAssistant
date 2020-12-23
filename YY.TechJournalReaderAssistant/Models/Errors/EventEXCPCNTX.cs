using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventEXCPCNTX : EventData
    {
        public string ServerComputerName => Properties.GetStringValueByKey("SERVERCOMPUTERNAME");
    }
}