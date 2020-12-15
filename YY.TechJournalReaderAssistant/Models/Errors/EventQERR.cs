namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventQERR : EventData
    {
        public string ServerComputerName => Properties.ContainsKey("query") ? Properties["query"] : null;
    }
}