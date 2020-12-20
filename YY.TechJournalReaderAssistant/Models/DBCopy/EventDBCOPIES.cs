using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBCopy
{
    public class EventDBCOPIES : EventData
    {
        public long? BlockNumber => Properties.GetLongValueByKey("BLOCKNUM");
        public long? CopyId => Properties.GetLongValueByKey("COPYID");
        public string CopyName => Properties.GetStringValueByKey("COPYNAME");
        public bool? CreateDB => Properties.GetBoolValueByKey("CREATEDB");
        public string DBName => Properties.GetStringValueByKey("DBNAME");
        public string DBServer => Properties.GetStringValueByKey("DBSERVER");
        public string DBType => Properties.GetStringValueByKey("DBTYPE");
        public string DBUser => Properties.GetStringValueByKey("DBUSER");
        public bool? FirstFillLaunch => Properties.GetBoolValueByKey("FIRSTFILLLAUNCH");
        public string FirstKey => Properties.GetStringValueByKey("FIRSTKEY");
        public string FirstPortionKey => Properties.GetStringValueByKey("FIRSTPORTIONKEY");
        public bool? FirstRun => Properties.GetBoolValueByKey("FIRSTRUN");
        public long? JobsCount => Properties.GetLongValueByKey("JOBSCOUNT");
        public string LastKey => Properties.GetStringValueByKey("LASTKEY");
        public long? OpCount => Properties.GetLongValueByKey("OPCOUNT");
        public string ReplicationType => Properties.GetStringValueByKey("REPLICATIONTYPE");
        public long? RowsCount => Properties.GetLongValueByKey("ROWSCOUNT");
        public bool? SomeJobFailed => Properties.GetBoolValueByKey("SOMEJOBFAILED");
        public bool? TableEmpty => Properties.GetBoolValueByKey("TABLEEMPTY");
        public string TableName => Properties.GetStringValueByKey("TABLENAME");
        public string TableType => Properties.GetStringValueByKey("TABLETYPE");
        public long? TrNum => Properties.GetLongValueByKey("TRNUM");
        public long? UnfinishedBlocksCount => Properties.GetLongValueByKey("UNFINISHEDBLOCKS");
    }
}