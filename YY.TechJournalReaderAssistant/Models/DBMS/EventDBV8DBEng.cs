using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBV8DBEng : EventData
    {
        public long? NumberParams => Properties.GetLongValueByKey("NPARAMS");
        public string PlanSQLText => Properties.GetStringValueByKey("PLANSQLTEXT");
        public long? Rows => Properties.GetLongValueByKey("ROWS");
        public long? RowsAffected => Properties.GetLongValueByKey("ROWSAFFECTED");
        public string SQLText => Properties.GetStringValueByKey("SQL");
    }
}