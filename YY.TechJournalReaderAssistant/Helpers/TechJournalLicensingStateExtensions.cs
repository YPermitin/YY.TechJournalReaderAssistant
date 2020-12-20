using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalLicensingStateExtensions
    {
        private static Dictionary<TechJournalLicensingState, string> _licensingStatePresentation = new Dictionary<TechJournalLicensingState, string>()
        {
            { TechJournalLicensingState.binding, "Проверка соответствия оборудования" },
            { TechJournalLicensingState.error, "Ошибка" },
            { TechJournalLicensingState.release, "Освобождена используемая лицензия" },
            { TechJournalLicensingState.reuse, "Переиспользована занятая ранее лицензия" },
            { TechJournalLicensingState.seize, "Занята новая лицензия" },
            { TechJournalLicensingState.Unknown, "Неизвестно" }

        };
        public static string GetPresentation(this TechJournalLicensingState eventSeverity)
        {
            if (_licensingStatePresentation.TryGetValue(eventSeverity, out string presentation))
                return presentation;
            else
                return _licensingStatePresentation[TechJournalLicensingState.Unknown];
        }
        public static TechJournalLicensingState Parse(string eventSeverityName)
        {
            if (string.IsNullOrEmpty(eventSeverityName))
                return TechJournalLicensingState.Unknown;
            else if (Enum.TryParse(eventSeverityName, true, out TechJournalLicensingState enumOut))
                return enumOut;
            else
                return TechJournalLicensingState.Unknown;
        }
    }
}
