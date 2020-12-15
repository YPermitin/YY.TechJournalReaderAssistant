namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventSDBL : EventData
    {
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
        public string SDBL => Properties.ContainsKey("sdbl") ? Properties["sdbl"] : null;
    }
}