namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventSCRIPTCIRCREFS : EventData
    {
        public string Cycles => Properties.ContainsKey("cycles") ? Properties["cycles"] : null;
        public string ModuleName => Properties.ContainsKey("ModuleName") ? Properties["ModuleName"] : null;
    }
}