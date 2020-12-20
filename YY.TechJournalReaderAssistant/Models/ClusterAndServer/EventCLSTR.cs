using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventCLSTR : EventData
    {
        public string ApplicationExt => Properties.GetStringValueByKey("APPLICATIONEXT");
        public long? ConnectionsLimit => Properties.GetLongValueByKey("CONNLIMIT");
        public string ClusterActionValue => Properties.GetStringValueByKey("EVENT");
        public TechJournalClusterActions ClusterAction => TechJournalClusterActionsExtensions.Parse(ClusterActionValue);
        public long? InfobaseLimitWorkProcess => Properties.GetLongValueByKey("IBLIMIT");
        public string ServerStateVersion => Properties.GetStringValueByKey("MYVER");
        public string ClusterEventName => Properties.GetStringValueByKey("NAME");
        public bool? NeedResync => Properties.GetBoolValueByKey("NEEDRESYNC");
        public string ClusterUnavailableReasonValue => Properties.GetStringValueByKey("REASON");
        public TechJournalClusterUnavailableReason ClusterUnavailableReason => TechJournalClusterUnavailableReasonExtensions.Parse(ClusterUnavailableReasonValue);
        public string ServiceName => Properties.GetStringValueByKey("SERVICENAME");
        public string ServerStateLast => Properties.GetStringValueByKey("SRCVER");
        public string SourceURLWorkServer => Properties.GetStringValueByKey("SRCURL");
    }
}