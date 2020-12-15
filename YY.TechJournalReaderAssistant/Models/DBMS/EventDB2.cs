using System;
using System.Text.RegularExpressions;
using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDB2 : EventData
    {
        private static readonly Regex _replaceTempTableName = new Regex(@"#tt[\d]+");
        private static readonly Regex _replaceParameterName = new Regex(@"@P[\d]+");

        private static string _sqlQueryOnly;
        private static string _sqlQueryParametersOnly;

        public bool? LKA
        {
            get
            {
                if (Properties.ContainsKey("lka")
                    && bool.TryParse(Properties["lka"], out var lkaValue))
                    return lkaValue;

                return null;
            }
        }
        public int? LKAID
        {
            get
            {
                if (Properties.ContainsKey("lkaid")
                    && int.TryParse(Properties["lkaid"], out var lkaIdValue))
                    return lkaIdValue;

                return null;
            }
        }
        public int? LKATO
        {
            get
            {
                if (Properties.ContainsKey("lkato")
                    && int.TryParse(Properties["lkato"], out var lkaIdValue))
                    return lkaIdValue;

                return null;
            }
        }
        public bool? LKP
        {
            get
            {
                if (Properties.ContainsKey("lkp")
                    && bool.TryParse(Properties["lkp"], out var lkaValue))
                    return lkaValue;

                return null;
            }
        }
        public string LKPID => Properties.ContainsKey("lkpid") ? Properties["lkpid"] : null;
        public int? LKPTO
        {
            get
            {
                if (Properties.ContainsKey("lkpto")
                    && int.TryParse(Properties["lkpto"], out var lkaIdValue))
                    return lkaIdValue;

                return null;
            }
        }
        public int? LKSRC
        {
            get
            {
                if (Properties.ContainsKey("lksrc")
                    && int.TryParse(Properties["lksrc"], out var lkaIdValue))
                    return lkaIdValue;

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
        public string SQLQueryOnly
        {
            get
            {
                if (_sqlQueryOnly == null && Properties.ContainsKey("Sql"))
                {
                    string bufferSql = (string)Properties["Sql"].Clone();
                    int endOfQuery = bufferSql.IndexOf("p_0", StringComparison.Ordinal);
                    _sqlQueryOnly = bufferSql.Substring(0, endOfQuery);
                }

                return _sqlQueryOnly;
            }
        }
        public string SQLQueryParametersOnly
        {
            get
            {
                if (_sqlQueryParametersOnly == null && Properties.ContainsKey("Sql"))
                {
                    string bufferSql = (string)Properties["Sql"].Clone();
                    int endOfQuery = bufferSql.IndexOf("p_0", StringComparison.Ordinal);
                    int lengthOfParams = bufferSql.Length - endOfQuery;
                    _sqlQueryParametersOnly = bufferSql.Substring(endOfQuery, lengthOfParams);
                }

                return _sqlQueryParametersOnly;
            }
        }
        public string SQLQueryHash
        {
            get
            {
                if (!string.IsNullOrEmpty(SQLQueryOnly))
                {
                    string bufferSql = (string)SQLQueryOnly.Clone();
                    _replaceParameterName.Replace(bufferSql, bufferSql);
                    _replaceTempTableName.Replace(bufferSql, bufferSql);
                    bufferSql = bufferSql.Replace(" ", "");
                    return bufferSql.CreateMD5();
                }

                return null;
            }
        }
    }
}