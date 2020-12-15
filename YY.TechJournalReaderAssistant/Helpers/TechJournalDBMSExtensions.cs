using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalDBMSExtensions
    {
        private static Dictionary<TechJournalDBMS, string> _dbmsPresentation = new Dictionary<TechJournalDBMS, string>()
        {
            { TechJournalDBMS.None, "Отсутствует" },
            { TechJournalDBMS.Unknown, "Неизвестно" },
            { TechJournalDBMS.DB2, "IBM DB2" },
            { TechJournalDBMS.DBDA, "DBDA" },
            { TechJournalDBMS.DBIBMInfosphereWarehouse, "IBM Infosphere Warehouse" },
            { TechJournalDBMS.DBMSSQL, "Microsoft SQL Server" },
            { TechJournalDBMS.DBMSSQLServerAnalysisServices, "Microsoft SQL Server Analysis Services" },
            { TechJournalDBMS.DBOracle, "Oracle Database" },
            { TechJournalDBMS.DBOracleEssbase, "Oracle Essbase" },
            { TechJournalDBMS.DBPOSTGRS, "PostgreSQL" },
            { TechJournalDBMS.DBUnkn, "Прочие СУБД" },
            { TechJournalDBMS.DBV8DBEng, "V8DBEng" }
        };
        
        public static string GetPresentation(this TechJournalDBMS dbms)
        {
            if (_dbmsPresentation.TryGetValue(dbms, out string presentation))
                return presentation;
            else
                return _dbmsPresentation[TechJournalDBMS.Unknown];
        }
        public static TechJournalDBMS Parse(string dbmsName)
        {
            if (string.IsNullOrEmpty(dbmsName))
                return TechJournalDBMS.None;
            else if (Enum.TryParse(dbmsName, true, out TechJournalDBMS enumOut))
                return enumOut;
            else
                return TechJournalDBMS.Unknown;
        }
    }
}