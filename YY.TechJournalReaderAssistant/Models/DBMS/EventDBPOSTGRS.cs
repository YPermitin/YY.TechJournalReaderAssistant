using System;
using System.Text.RegularExpressions;
using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBPOSTGRS : EventData
    {
        private static readonly Regex _replaceTempTableName = new Regex(@"#tt[\d]+");
        private static readonly Regex _replaceParameterName = new Regex(@"@P[\d]+");

        private string _sqlQueryOnly;
        private string _sqlQueryParametersOnly;

        public bool? LKA => Properties.GetBoolValueByKey("LKA");
        public long? LKAID => Properties.GetLongValueByKey("LKAID");
        public long? LKATO => Properties.GetLongValueByKey("LKATO");
        public bool? LKP => Properties.GetBoolValueByKey("LKP");
        public string LKPID => Properties.GetStringValueByKey("LKPID");
        public long? LKPTO => Properties.GetLongValueByKey("LKPTO");
        public long? LKSRC => Properties.GetLongValueByKey("LKSRC");
        public string PlanSQLText => Properties.GetStringValueByKey("PLANSQLTEXT");
        public string SQLText => Properties.GetStringValueByKey("SQL");
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