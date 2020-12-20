using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;
using YY.TechJournalReaderAssistant.Models.Special;

namespace YY.TechJournalReaderAssistant.Helpers
{
    public static class TechJournalClusterActionsExtensions
    {
        private static Dictionary<TechJournalClusterActions, string> _techJournalClusterActionsPresentations = new Dictionary<TechJournalClusterActions, string>()
        {
            { TechJournalClusterActions.CurrentVersionNewer, "Активный экземпляр сервиса получил репликацию со старой версией состояния сервиса и отверг ее" },
            { TechJournalClusterActions.CurrentVersionOlder, "Активный экземпляр сервиса получил репликацию с новой версией состояния сервиса, он должен стать резервным" },
            { TechJournalClusterActions.DataReplicationStart, "Начало репликации данных из текущего активного экземпляра сервиса в резервный экземпляр" },
            { TechJournalClusterActions.DestinationVersionNewer, "Репликация была передана в активный экземпляр сервиса с новой версией состояния сервиса, репликация была отвергнута и текущий сервис должен стать резервным" },
            { TechJournalClusterActions.DestinationVersionOlder, "Репликация была передана в активный экземпляр сервиса со старой версией состояния сервиса" },
            { TechJournalClusterActions.DistribObsolete, "Кеш назначений функцональности кластера устарел в текущем рабочем процессе" },
            { TechJournalClusterActions.FinishReplication, "Репликация закончена" },
            { TechJournalClusterActions.MainRmngrIsDown, "Ошибка вызова сервиса кластера на главном менеджере" },
            { TechJournalClusterActions.ProcessUnavailable, "Рабочий процесс не может быть использован для соединения с информационной базой" },
            { TechJournalClusterActions.RegisterRmngr, "Регистрация менеджера кластера" },
            { TechJournalClusterActions.RegisterRphost, "Регистрация рабочего процесса" },
            { TechJournalClusterActions.ServiceAssignRequire, "Сервис недоступен, требуется переназначение"},
            { TechJournalClusterActions.UnregisterRmngr, "Отмена регистрации менеджера кластера" },
            { TechJournalClusterActions.UnregisterRphost, "Отмена регистрации рабочего процесса" },
            { TechJournalClusterActions.WorkingProcessNotFound, "Не найден рабочий процесс для соединения с информационной базой" },
            { TechJournalClusterActions.Unknown, "Неизвестно" }
        };

        private static Dictionary<string, TechJournalClusterActions> _techJournalClusterActionsAsStringValues = new Dictionary<string, TechJournalClusterActions>()
        {
            { "CURRENVERSIONNEWER", TechJournalClusterActions.CurrentVersionNewer },
            { "CURRENTVERSIONOLDER", TechJournalClusterActions.CurrentVersionOlder },
            { "DATAREPLICATIONSTART", TechJournalClusterActions.DataReplicationStart },
            { "DESTINATIONVERSIONNEWER", TechJournalClusterActions.DestinationVersionNewer },
            { "DESTINATIONVERSIONOLDER", TechJournalClusterActions.DestinationVersionOlder },
            { "DISTRIBOBSOLETE", TechJournalClusterActions.DistribObsolete },
            { "FINISHREPLICATION", TechJournalClusterActions.FinishReplication },
            { "MAINRMNGRISDOWN", TechJournalClusterActions.MainRmngrIsDown },
            { "PROCESSUNAVAILABLE", TechJournalClusterActions.ProcessUnavailable },
            { "REGISTERRMNGR", TechJournalClusterActions.RegisterRmngr },
            { "REGISTERRPHOST", TechJournalClusterActions.RegisterRphost },
            { "SERVICEASSIGNREQUIRE", TechJournalClusterActions.ServiceAssignRequire },
            { "UNREGISTERRMNGR", TechJournalClusterActions.UnregisterRmngr },
            { "UNREGISTERRPHOST", TechJournalClusterActions.UnregisterRphost },
            { "WORKINGPROCESSNOTFOUND", TechJournalClusterActions.WorkingProcessNotFound }
        };

        public static string GetPresentation(this TechJournalClusterActions action)
        {
            if (_techJournalClusterActionsPresentations.TryGetValue(action, out string presentation))
                return presentation;
            else
                return _techJournalClusterActionsPresentations[TechJournalClusterActions.Unknown];
        }
        public static TechJournalClusterActions Parse(string action)
        {
            if (string.IsNullOrEmpty(action))
                return TechJournalClusterActions.Unknown;
            else if (Enum.TryParse(action, true, out TechJournalClusterActions enumOut))
                return enumOut;
            else if (_techJournalClusterActionsAsStringValues.TryGetValue(action.Replace(" ", "").ToUpper(),
                out enumOut))
                return enumOut;
            else
                return TechJournalClusterActions.Unknown;
        }
    }
}
