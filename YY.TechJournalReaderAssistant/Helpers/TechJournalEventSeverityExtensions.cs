using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalEventSeverityExtensions
    {
        private static Dictionary<TechJournalEventSeverity, string> _eventSeverityPresentation = new Dictionary<TechJournalEventSeverity, string>()
        {
            { TechJournalEventSeverity.debug, "Уровень отладочной информации" },
            { TechJournalEventSeverity.error, "Уровень ошибок" },
            { TechJournalEventSeverity.info, "Информационный уровень" },
            { TechJournalEventSeverity.none, "Отключение событий" },
            { TechJournalEventSeverity.trace, "Максимально подробный уровень" },
            { TechJournalEventSeverity.warning, "Уровень предупреждений" },
            { TechJournalEventSeverity.unknown, "Неизвестно" }
        };
        public static string GetPresentation(this TechJournalEventSeverity eventSeverity)
        {
            if (_eventSeverityPresentation.TryGetValue(eventSeverity, out string presentation))
                return presentation;
            else
                return _eventSeverityPresentation[TechJournalEventSeverity.unknown];
        }
        public static TechJournalEventSeverity Parse(string eventSeverityName)
        {
            if (string.IsNullOrEmpty(eventSeverityName))
                return TechJournalEventSeverity.unknown;
            else if (Enum.TryParse(eventSeverityName, true, out TechJournalEventSeverity enumOut))
                return enumOut;
            else
                return TechJournalEventSeverity.unknown;
        }
    }
}
