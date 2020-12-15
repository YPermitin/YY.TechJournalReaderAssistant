using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models
{
    public class EventDbMSSQLData : EventData
    {
        public string Database => Properties.ContainsKey("Database") ? Properties["Database"] : null;
        public string DatabaseCopy => Properties.ContainsKey("DBCopy") ? Properties["DBCopy"] : null;
        public string DBMSValue => Properties.ContainsKey("dbms") ? Properties["dbms"] : null;
        public TechJournalDBMS DBMS => TechJournalDBMSExtensions.Parse(DBMSValue);
        public string DatabasePID => Properties.ContainsKey("dbpid") ? Properties["dbpid"] : null;
        
        // TODO: Добавить остальные 19 свойств события DBMSSQL
    }
}