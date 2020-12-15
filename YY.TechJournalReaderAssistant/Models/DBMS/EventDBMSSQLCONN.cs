namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBMSSQLCONN : EventData
    {
        public string HResultOLEDB => Properties.ContainsKey("hResultOLEDB") ? Properties["hResultOLEDB"] : null;
        public string HResultNC2005 => Properties.ContainsKey("hResultNC2005") ? Properties["hResultNC2005"] : null;
        public string HResultNC2008 => Properties.ContainsKey("hResultNC2008") ? Properties["hResultNC2008"] : null;
        public string HResultNC2012 => Properties.ContainsKey("hResultNC2012") ? Properties["hResultNC2012"] : null;
    }
}