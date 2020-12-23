using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.SessionData
{
    public class EventSDGC : EventData
    {
        public long? CopyBytes => Properties.GetLongValueByKey("COPYBYTES");
        public long? FilesSize => Properties.GetLongValueByKey("FILESSIZE");
        public long? InstanceID => Properties.GetLongValueByKey("INSTANCEID");
        public long? LockDuration => Properties.GetLongValueByKey("LOCKDURATION");
        public string Method => Properties.GetStringValueByKey("METHOD");
        public long? UsedSize => Properties.GetLongValueByKey("USEDSIZE");
    }
}