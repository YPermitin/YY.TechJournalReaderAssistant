using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.InteractiveActions
{
    public class EventINPUTBYSTRING : EventData
    {
        public long? FindTicks => Properties.GetLongValueByKey("FINDTICKS");
        public long? FullTextResultCount => Properties.GetLongValueByKey("FTEXTRESULTCOUNT");
        public long? FullTextSearchCount => Properties.GetLongValueByKey("FTEXTSEARCHCOUNT");
        public long? FullTextTicks => Properties.GetLongValueByKey("FTEXTTICKS");
        public bool? SearchByMask => Properties.GetBoolValueByKey("SEARCHBYMASK");
        public string Text => Properties.GetStringValueByKey("TEXT");
        public bool? TooManyResults => Properties.GetBoolValueByKey("TOOMANYRESULTS");
    }
}