using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.DBMS
{
    public class EventDBMSSQLCONN : EventData
    {
        public string HResultOLEDB => Properties.GetStringValueByKey("HRESULTOLEDB");
        public string HResultNC2005 => Properties.GetStringValueByKey("HRESULTNC2005");
        public string HResultNC2008 => Properties.GetStringValueByKey("HRESULTNC2008");
        public string HResultNC2012 => Properties.GetStringValueByKey("HRESULTNC2012");
    }
}