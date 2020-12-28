using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBMSSQL : EventData
    {
        public bool? LKA => Properties.GetBoolValueByKey("LKA");
        public long? LKAID => Properties.GetLongValueByKey("LKAID");
        public long? LKATO => Properties.GetLongValueByKey("LKATO");
        public bool? LKP => Properties.GetBoolValueByKey("LKP");
        public string LKPID => Properties.GetStringValueByKey("LKPID");
        public long? LKPTO => Properties.GetLongValueByKey("LKPTO");
        public long? LKSRC => Properties.GetLongValueByKey("LKSRC");
    }
}