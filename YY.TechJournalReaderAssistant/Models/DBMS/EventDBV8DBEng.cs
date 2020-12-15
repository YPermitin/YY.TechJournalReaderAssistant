namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBV8DBEng : EventData
    {
        public int? NumberParams
        {
            get
            {
                if (Properties.ContainsKey("NParams")
                    && int.TryParse(Properties["NParams"], out var NParamsValue))
                    return NParamsValue;

                return null;
            }
        }
        public string PlanSQLText => Properties.ContainsKey("planSQLText") ? Properties["planSQLText"] : null;
        public int? Rows
        {
            get
            {
                if (Properties.ContainsKey("rows")
                    && int.TryParse(Properties["rows"], out var rowsValue))
                    return rowsValue;

                return null;
            }
        }
        public int? RowsAffected
        {
            get
            {
                if (Properties.ContainsKey("rowsaffected")
                    && int.TryParse(Properties["rowsaffected"], out var rowsAffectedValue))
                    return rowsAffectedValue;

                return null;
            }
        }
        public string SQLText => Properties.ContainsKey("sql") ? Properties["sql"] : null;
    }
}