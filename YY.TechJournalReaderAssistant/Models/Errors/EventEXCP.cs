namespace YY.TechJournalReaderAssistant.Models.Errors
{
    public class EventEXCP : EventData
    {
        public string Description => Properties.ContainsKey("descr") ? Properties["descr"] : null;
        public string DumpError => Properties.ContainsKey("dumperror") ? Properties["dumperror"] : null;
        public string DumpFile => Properties.ContainsKey("dumpfile") ? Properties["dumpfile"] : null;
        public string Exception => Properties.ContainsKey("exception") ? Properties["exception"] : null;
        public string OSException => Properties.ContainsKey("osexception") ? Properties["osexception"] : null;
    }
}