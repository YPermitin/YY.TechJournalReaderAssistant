using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ExternalDataSource
{
    public class EventEDS : EventData
    {
        public string DBConnectionString => Properties.GetStringValueByKey("DBCONNSTR");
        public string DBUser => Properties.GetStringValueByKey("DBUSR");
        public string DBConnectionId => Properties.GetStringValueByKey("DBCONNID");
        public string Exception => Properties.GetStringValueByKey("EXCEPTION");
        public string MDX => Properties.GetStringValueByKey("MDX");
    }
}