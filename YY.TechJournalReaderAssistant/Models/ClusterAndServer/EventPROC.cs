using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventPROC : EventData
    {
        public bool? IsErrorMessage => Properties.GetBoolValueByKey("ERR");
        public string FinishReason => Properties.GetStringValueByKey("FINISH");
        public long? MainPort => Properties.GetLongValueByKey("PORT");
        public string RunAs => Properties.GetStringValueByKey("RUNAS");
        public long? SecondPort => Properties.GetLongValueByKey("SYNCPORT");
        public string Message => Properties.GetStringValueByKey("TXT");
    }
}