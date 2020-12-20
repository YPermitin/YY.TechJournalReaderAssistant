using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalClusterMessageTypeExtensions
    {
        private static Dictionary<TechJournalClusterMessageType, string> _techJournalClusterMessageTypePresentations = new Dictionary<TechJournalClusterMessageType, string>()
        {
            { TechJournalClusterMessageType.cluster_limit, "Сообщение связано с превышением предельных значений ограничений" },
            { TechJournalClusterMessageType.process_limit, "Событие связано с прерыванием проблемных rphost" },
            { TechJournalClusterMessageType.Unknown, "Неизвестно" }
        };
        
        public static string GetPresentation(this TechJournalClusterMessageType type)
        {
            if (_techJournalClusterMessageTypePresentations.TryGetValue(type, out string presentation))
                return presentation;
            else
                return _techJournalClusterMessageTypePresentations[TechJournalClusterMessageType.Unknown];
        }
        public static TechJournalClusterMessageType Parse(string type)
        {
            if (string.IsNullOrEmpty(type))
                return TechJournalClusterMessageType.Unknown;
            else if (Enum.TryParse(type, true, out TechJournalClusterMessageType enumOut))
                return enumOut;
            else
                return TechJournalClusterMessageType.Unknown;
        }
    }
}
