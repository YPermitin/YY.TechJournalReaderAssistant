using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models.FullText
{
    public class EventFTEXTUpd : EventData
    {
        public long? AvMem => Properties.GetLongValueByKey("AVMEM");
        public bool? BackgroundJobCreated => Properties.GetBoolValueByKey("BACKGROUNDJOBCREATED");
        public long? FailedJobsCount => Properties.GetLongValueByKey("FAILEDJOBSCOUNT");
        public string Files => Properties.GetStringValueByKey("FILES");
        public long? FilesCount => Properties.GetLongValueByKey("FILESCOUNT");
        public long? FilesTotalSize => Properties.GetLongValueByKey("FILESTOTALSIZE");
        public string Folder => Properties.ContainsKey("Folder") ? Properties["Folder"] : null;
        public bool? JobCanceledByLoadLimit => Properties.GetBoolValueByKey("JOBCANCELEDBYLOADLIMIT");
        public long? minDataId => Properties.GetLongValueByKey("MINDATAID");
        public long? MemoryUsed => Properties.GetLongValueByKey("MEMORYUSED");
        public string FullTextUpdateStateValue => Properties.GetStringValueByKey("STATE");
        public TechJournalFullTextUpdateState FullTextUpdateState => TechJournalFullTextUpdateStateExtensions.Parse(FullTextUpdateStateValue);
        public string Time => Properties.GetStringValueByKey("TIME");
        public long? TotalJobsCount => Properties.GetLongValueByKey("TOTALJOBSCOUNT");
    }
}