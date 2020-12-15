namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventEXCPCNTX : EventData
    {
        public string ServerComputerName => Properties.ContainsKey("ServerComputerName") ? Properties["ServerComputerName"] : null;
    }
}