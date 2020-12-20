using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ExternalDataSource
{
    public class EventEDS : EventData
    {
        public string DBConnectionString => Properties.GetStringValueByKey("DBCONNSTR");
        public string DBUser => Properties.GetStringValueByKey("DBUSR");
        public string DBConnectionId => Properties.GetStringValueByKey("DBCONNID");
        public string Description => Properties.GetStringValueByKey("DESCR");
        public string Exception => Properties.GetStringValueByKey("EXCEPTION");
        public string MDX => Properties.GetStringValueByKey("MDX");
        public string PlanSQLText => Properties.GetStringValueByKey("PLANSQLTEXT");
        public long? Rows => Properties.GetLongValueByKey("ROWS");
        public long? RowsAffected => Properties.GetLongValueByKey("ROWSAFFECTED");
        public string SQL => Properties.GetStringValueByKey("SQL");
    }
}