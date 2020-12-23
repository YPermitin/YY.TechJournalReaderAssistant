using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalClusterUnavailableReasonExtensions
    {
        private static Dictionary<TechJournalClusterUnavailableReason, string> _techJournalClusterUnavailableReasonPresentations = new Dictionary<TechJournalClusterUnavailableReason, string>()
        {
            { TechJournalClusterUnavailableReason.connlimit, "Достигнуто максимальное количество соединений на рабочий процесс" },
            { TechJournalClusterUnavailableReason.iblimit, "Достигнуто максимальное количество ИБ на рабочий процесс" },
            { TechJournalClusterUnavailableReason.Unknown, "Неизвестно" }
        };
        
        public static string GetPresentation(this TechJournalClusterUnavailableReason reason)
        {
            if (_techJournalClusterUnavailableReasonPresentations.TryGetValue(reason, out string presentation))
                return presentation;
            else
                return _techJournalClusterUnavailableReasonPresentations[TechJournalClusterUnavailableReason.Unknown];
        }
        public static TechJournalClusterUnavailableReason Parse(string reason)
        {
            if (string.IsNullOrEmpty(reason))
                return TechJournalClusterUnavailableReason.Unknown;
            else if (Enum.TryParse(reason, true, out TechJournalClusterUnavailableReason enumOut))
                return enumOut;
            else
                return TechJournalClusterUnavailableReason.Unknown;
        }
    }
}
