using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalFullTextUpdateStateExtensions
    {
        private static Dictionary<TechJournalFullTextUpdateState, string> _eventSeverityPresentation = new Dictionary<TechJournalFullTextUpdateState, string>()
        {
            { TechJournalFullTextUpdateState.Start, "Начало обновления индекса полнотекстового поиска" },
            { TechJournalFullTextUpdateState.End, "Окончание обновления индекса полнотекстового поиска" },
            { TechJournalFullTextUpdateState.Unknown, "Неизвестно" }

        };
        public static string GetPresentation(this TechJournalFullTextUpdateState eventSeverity)
        {
            if (_eventSeverityPresentation.TryGetValue(eventSeverity, out string presentation))
                return presentation;
            else
                return _eventSeverityPresentation[TechJournalFullTextUpdateState.Unknown];
        }
        public static TechJournalFullTextUpdateState Parse(string eventSeverityName)
        {
            if (string.IsNullOrEmpty(eventSeverityName))
                return TechJournalFullTextUpdateState.Unknown;
            else if (Enum.TryParse(eventSeverityName, true, out TechJournalFullTextUpdateState enumOut))
                return enumOut;
            else
                return TechJournalFullTextUpdateState.Unknown;
        }
    }
}
