using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant.Helpers {
	public static class TechJournalActionExtensions {
		private static readonly Dictionary < TechJournalAction,
		string > _actionDescription = new Dictionary < TechJournalAction,
		string > () {
			{
				TechJournalAction.AcceptPartialIndex,
				"Принять частичные индексы"
			},
			{
				TechJournalAction.addCopy,
				"Добавление копии"
			},
			{
				TechJournalAction.agentAuthenticate,
				"Аутентификация администратора центрального сервера"
			},
			{
				TechJournalAction.applyServiceAssociationRules,
				"Применение требований назначения функциональности"
			},
			{
				TechJournalAction.attach,
				"Назначение сеанса соединению (событие типа SESN выводится в момент отмены назначения соединению сеанса). Длительность показывает, сколько времени сеанс был назначен соединению"
			},
			{
				TechJournalAction.authenticateInfoBaseAdmin,
				"Аутентификация администратора информационной базы"
			},
			{
				TechJournalAction.authenticateSrvrUser,
				"Аутентификация пользователя кластера в рабочем сервере"
			},
			{
				TechJournalAction.authenticateSrvrUser,
				"Аутентификация администратора кластера в рабочем процессе"
			},
			{
				TechJournalAction.authenticateStarter,
				"Аутентификация удаленного центрального сервера"
			},
			{
				TechJournalAction.beginTransaction,
				"Начало транзакции (событие типа SDBL выводится в журнал в момент начала транзакции и не имеет длительности)"
			},
			{
				TechJournalAction.busy,
				"Сеанс уже назначен соединению (событие типа SESN выводится при попытке назначения соединению сеанса, который уже назначен). Не имеет длительности."
			},
			{
				TechJournalAction.changeInfoBaseParams,
				"Изменение параметров информационной базы: выдача лицензий сервером, внешнее управление сеансами, обязательное управление внешними сеансами, профиль безопасности, профиль безопасности безопасного режима."
			},
			{
				TechJournalAction.changeLocale,
				"Изменение национальных настроек базы данных"
			},
			{
				TechJournalAction.CheckIndexes,
				"Выполняется проверка индексов полнотекстового поиска"
			},
			{
				TechJournalAction.clearDataBase,
				"Очистить данные"
			},
			{
				TechJournalAction.commitTransaction,
				"Фиксация транзакции"
			},
			{
				TechJournalAction.connect,
				"Соединение с внешним источником данных или внешним сервисом интеграции"
			},
			{
				TechJournalAction.continueFillTable,
				"Возобновление первоначального заполнения"
			},
			{
				TechJournalAction.copyMoveFile,
				"Копирование/перемещение фрагмента конфигурации между записями таблиц базы данных"
			},
			{
				TechJournalAction.createFile,
				"Создание файла"
			},
			{
				TechJournalAction.createInfoBase,
				"Создание информационной базы"
			},
			{
				TechJournalAction.DB_copies_notification_background_job,
				"Обработка оповещения механизма копий базы данных"
			},
			{
				TechJournalAction.DB_copy_filling_background_job,
				"Первоначальное копирование таблицы копии базы данных"
			},
			{
				TechJournalAction.deleteFile,
				"Удаление файла"
			},
			{
				TechJournalAction.deserializeTable,
				"Восстановление данных таблицы базы данных из файла"
			},
			{
				TechJournalAction.disconnect,
				"Разрыв соединения с внешним источником данных или внешним сервисом интеграции"
			},
			{
				TechJournalAction.dropInfoBase,
				"Удаление информационной базы"
			},
			{
				TechJournalAction.eraseAgentUser,
				"Удаление пользователя центрального сервера"
			},
			{
				TechJournalAction.eraseAgentUser,
				"Удаление администратора центрального сервера"
			},
			{
				TechJournalAction.eraseIBRegistry,
				"Удаление кластера из центрального сервера"
			},
			{
				TechJournalAction.eraseIBRegistry,
				"Удаление кластера"
			},
			{
				TechJournalAction.eraseRegServer,
				"Удаление рабочего сервера"
			},
			{
				TechJournalAction.eraseRegUser,
				"Удаление пользователя (или администратора) кластера"
			},
			{
				TechJournalAction.eraseSeance,
				"Удаление сеанса"
			},
			{
				TechJournalAction.eraseSecurityProfile,
				"Удаление профиля безопасности"
			},
			{
				TechJournalAction.eraseSecurityProfileAddIn,
				"Удаление записи из профиля безопасности, внешняя компонента"
			},
			{
				TechJournalAction.eraseSecurityProfileApplication,
				"Удаление записи из профиля безопасности, приложение"
			},
			{
				TechJournalAction.eraseSecurityProfileExternalModule,
				"Удаление записи из профиля безопасности, внешний модуль"
			},
			{
				TechJournalAction.eraseSecurityProfileInternetResource,
				"Удаление записи из профиля безопасности, интрент-ресурс"
			},
			{
				TechJournalAction.eraseSecurityProfileVirtualDirectory,
				"Удаление записи из профиля безопасности, виртуальный каталог"
			},
			{
				TechJournalAction.eraseServerProcess,
				"Удаление рабочего процесса"
			},
			{
				TechJournalAction.eraseServiceAssociationRule,
				"Удаление требования назначения функциональности"
			},
			{
				TechJournalAction.fillTable,
				"Первоначальное заполнение таблицы"
			},
			{
				TechJournalAction.fillTableBlocksKeyFields,
				"Заполнение таблицы по ключам"
			},
			{
				TechJournalAction.fillTableBlocksKeyFieldsTableParts,
				"Заполнение ссылочной таблицы"
			},
			{
				TechJournalAction.fillTableOne,
				"Заполнение таблицы одним запросом"
			},
			{
				TechJournalAction.finish,
				"Окончание сеанса (событие типа SESN выводится в журнал в момент окончания сеанса, и длительность события равна длительности всего сеанса)"
			},
			{
				TechJournalAction.FtextMngrIndexChanges,
				"Выполняется обновление индекса полнотекстового поиска в файловом варианте информационной базы"
			},
			{
				TechJournalAction.FtextMngrRHostIndexChanges,
				"Выполняется обновление индекса полнотекстового поиска в клиент-серверном варианте информационной базы"
			},
			{
				TechJournalAction.getAgentUsers,
				"Чтение данных по администраторам агента"
			},
			{
				TechJournalAction.getClusterManagers,
				"Чтение списка и параметров менеджеров кластера"
			},
			{
				TechJournalAction.getConnections,
				"Чтение списка соединений"
			},
			{
				TechJournalAction.GetDataForIndexing,
				"Получить список измененных объектов для включения в индекс полнотекстового поиска"
			},
			{
				TechJournalAction.getIBRegistry,
				"Чтение списка и параметров кластеров"
			},
			{
				TechJournalAction.getInfoBaseParams,
				"Чтение параметров информационной базы"
			},
			{
				TechJournalAction.getInfoBases,
				"Чтение списка информационных баз"
			},
			{
				TechJournalAction.getObjectLocks,
				"Чтение списка объектных блокировок кластера"
			},
			{
				TechJournalAction.getRegUsers,
				"Чтение данных по администраторам кластера"
			},
			{
				TechJournalAction.getSeances,
				"Чтение списка сеансов"
			},
			{
				TechJournalAction.getSecurityProfile,
				"Чтение полного списка профилей безопасности или их записей"
			},
			{
				TechJournalAction.getSecurityProfileAddIn,
				"Чтение полного списка профилей безопасности или их записей, внешняя компонента"
			},
			{
				TechJournalAction.getSecurityProfileApplication,
				"Чтение полного списка профилей безопасности или их записей, приложение"
			},
			{
				TechJournalAction.getSecurityProfileComClass,
				"Чтение полного списка профилей безопасности или их записей, COM-класс"
			},
			{
				TechJournalAction.getSecurityProfileExternalModule,
				"Чтение полного списка профилей безопасности или их записей, внешний модуль"
			},
			{
				TechJournalAction.getSecurityProfileInternetResource,
				"Чтение полного списка профилей безопасности или их записей, интрент-ресурс"
			},
			{
				TechJournalAction.getSecurityProfileVirtualDirectory,
				"Чтение полного списка профилей безопасности или их записей, виртуальный каталог"
			},
			{
				TechJournalAction.getServerProcesses,
				"Чтение списка и параметров рабочих процессов"
			},
			{
				TechJournalAction.getServiceAssociationRules,
				"Чтение списка требований назначения функциональности"
			},
			{
				TechJournalAction.getServicesDistribution,
				"Чтение данных по распределению сервисов по менеджерам кластера"
			},
			{
				TechJournalAction.getServicesInfo,
				"Чтение информации о доступных сервисах кластера"
			},
			{
				TechJournalAction.getTransactionSplitter,
				"Получение разделителя итогов"
			},
			{
				TechJournalAction.holdConnection,
				"Удержание соединения"
			},
			{
				TechJournalAction.IndexObjects,
				"Выполняется индексация порции объектов"
			},
			{
				TechJournalAction.initialize,
				"Инициализация подсистемы лицензирования (только для события LIC)."
			},
			{
				TechJournalAction.insertAgentUser,
				"Добавление пользователя (или администратора) центрального сервера"
			},
			{
				TechJournalAction.insertIBRegistry,
				"Добавление кластера в центральный сервер или создание кластера"
			},
			{
				TechJournalAction.insertMessageIntoSendingQueue,
				"Добавление сообщения в очередь отправки сообщений сервиса интеграции"
			},
			{
				TechJournalAction.insertRecords,
				"Добавление записи в таблицу базы данных"
			},
			{
				TechJournalAction.insertRegServer,
				"Добавление рабочего сервера"
			},
			{
				TechJournalAction.insertRegUser,
				"Добавление пользователия (или администратора) кластера"
			},
			{
				TechJournalAction.insertServerProcess,
				"Добавление рабочего процесса"
			},
			{
				TechJournalAction.Integration_service_message_receiving_queues_processing_job,
				"Обработка очередей получения сообщений сервиса интеграции"
			},
			{
				TechJournalAction.Integration_service_messages_receiving_job,
				"Получение сообщений сервиса интеграции"
			},
			{
				TechJournalAction.Integration_service_received_messages_processing_job,
				"Обработка полученных сообщений сервиса интеграции"
			},
			{
				TechJournalAction.Integration_service_sending_queues_processing_job,
				"Обработка очередей отправки сообщений сервиса интеграции"
			},
			{
				TechJournalAction.isProperLocale,
				"Проверка национальных настроек, установленных для базы данных"
			},
			{
				TechJournalAction.killClient,
				"Разрыв соединения клиента с кластером серверов системы «1С:Предприятие»"
			},
			{
				TechJournalAction.lockRecord,
				"Блокировка записи"
			},
			{
				TechJournalAction.lookupTmpTable,
				"Получение / создание временной таблицы базы данных"
			},
			{
				TechJournalAction.MergeSynchro,
				"Объединить файлы с индексами полнотекстового поиска"
			},
			{
				TechJournalAction.modifyFile,
				"Обновление файла"
			},
			{
				TechJournalAction.moveFile,
				"Перемещение файла"
			},
			{
				TechJournalAction.processReceivedMessage,
				"Обработка полученого сообщения сервиса интеграции"
			},
			{
				TechJournalAction.quickInsert,
				"Быстрая вставка данных в таблицу базы данных"
			},
			{
				TechJournalAction.readFile,
				"Чтение файла"
			},
			{
				TechJournalAction.Recalc_totals_system_background_job,
				"Фоновый пересчет итогов"
			},
			{
				TechJournalAction.receiveMessage,
				"Получение сообщения сервиса интеграции"
			},
			{
				TechJournalAction.reFillTable,
				"Очистка таблицы и возобновление заполнения"
			},
			{
				TechJournalAction.regAuthenticate,
				"Аутентификация в кластере"
			},
			{
				TechJournalAction.removeCopy,
				"Удаление копии"
			},
			{
				TechJournalAction.restoreObject,
				"Восстановление объекта"
			},
			{
				TechJournalAction.resumeIndexing,
				"Восстановление индексирования таблиц базы данных"
			},
			{
				TechJournalAction.returnTmpTable,
				"Освобождение временной таблицы базы данных"
			},
			{
				TechJournalAction.rollbackTransaction,
				"Отмена транзакции"
			},
			{
				TechJournalAction.saveObject,
				"Сохранение объекта"
			},
			{
				TechJournalAction.searchFile,
				"Поиск файла"
			},
			{
				TechJournalAction.securedInsert,
				"Вставка записей с наложением ограничений доступа к данным"
			},
			{
				TechJournalAction.selectFileName,
				"Выбор имени файла"
			},
			{
				TechJournalAction.sendMessage,
				"Отправка сообщения из очереди отправки сообщений сервиса интеграции"
			},
			{
				TechJournalAction.serializeTable,
				"Сохранение данных таблицы в файл"
			},
			{
				TechJournalAction.setClusterRecycling,
				"Изменение настроек перезапуска рабочих процессов кластера (кроме уровня отказоустойчивости)"
			},
			{
				TechJournalAction.setFaultToleranceLevel,
				"Изменение уровня отказоустойчивости кластера"
			},
			{
				TechJournalAction.setInfoBaseConnectingDeny,
				"Изменение параметров блокировки начала сеансов информационной базы"
			},
			{
				TechJournalAction.setInfoBaseDescr,
				"Изменение описания информационной базы"
			},
			{
				TechJournalAction.setInfoBaseScheduledJobsDeny,
				"Изменение блокировки регламентных заданий информационной базы"
			},
			{
				TechJournalAction.setRegDescr,
				"Изменение описания кластера"
			},
			{
				TechJournalAction.setRegMultiProcEnable,
				"Установка значения флажка поддержки кластером многих рабочих процессов"
			},
			{
				TechJournalAction.setRegSecLevel,
				"Изменение уровня защищенного соединения кластера"
			},
			{
				TechJournalAction.setRollbackOnly,
				"Установка флажка наличия в транзакции ошибки (ее можно только откатить)"
			},
			{
				TechJournalAction.setSecurityProfile,
				"Создание и изменение профиля безопасности"
			},
			{
				TechJournalAction.setSecurityProfileAddIn,
				"Создание / изменение записи в профиле безопасности (внешняя компонента)"
			},
			{
				TechJournalAction.setSecurityProfileApplication,
				"Создание / изменение записи в профиле безопасности (приложение)"
			},
			{
				TechJournalAction.setSecurityProfileComClass,
				"Создание / изменение записи в профиле безопасности (COM-класс)"
			},
			{
				TechJournalAction.setSecurityProfileExternalModule,
				"Создание / изменение записи в профиле безопасности (внешний модуль)"
			},
			{
				TechJournalAction.setSecurityProfileInternetResource,
				"Создание / изменение записи в профиле безопасности (интернет ресурс)"
			},
			{
				TechJournalAction.setSecurityProfileVirtualDirectory,
				"Создание / изменение записи в профиле безопасности (виртуальный каталог)"
			},
			{
				TechJournalAction.setServerProcessCapacity,
				"Установка производительности (пропускной способности) рабочего процесса"
			},
			{
				TechJournalAction.setServerProcessEnable,
				"Установка статуса (или флажка разрешения запуска) рабочего процесса"
			},
			{
				TechJournalAction.setServiceAssociationRule,
				"Создание и изменение требования назначения функциональности"
			},
			{
				TechJournalAction.setSingleUser,
				"Установка монопольного режима"
			},
			{
				TechJournalAction.setSrcProcessName,
				"Означает создание общих данных информационной базы в рабочем процессе и назначение им общего имени. Событие записывается при подключении первого пользователя к информационной базе через данный рабочий процесс или при выполнении динамического обновлен"
			},
			{
				TechJournalAction.setTableState,
				"Изменение состояния таблицы копии"
			},
			{
				TechJournalAction.start,
				"Начало сеанса (событие типа SESN выводится в журнал в момент начала сеанса и не имеет длительности)"
			},
			{
				TechJournalAction.suspendIndexing,
				"Отмена индексирования таблиц базы данных"
			},
			{
				TechJournalAction.takeKeyVal,
				"Получение значения ключа записи табличной части"
			},
			{
				TechJournalAction.transaction,
				"Начало транзакции (событие типа SDBL начинается при начале транзакции, заканчивается при завершении транзакции)"
			},
			{
				TechJournalAction.transferChangesTable,
				"Перенос объектов изменений"
			},
			{
				TechJournalAction.transferTrLogs,
				"Перенос журналов транзакций"
			},
			{
				TechJournalAction.updateCopyContent,
				"Изменение состава копии"
			},
			{
				TechJournalAction.updateCopyProperties,
				"Изменение параметров копии"
			},
			{
				TechJournalAction.updateRegServer,
				"Изменение параметров рабочего сервера"
			},
			{
				TechJournalAction.updateTimeIsOver,
				"Завершение времени обновления"
			},
			{
				TechJournalAction.wait,
				"Ожидание назначения (событие типа SESN выводится в момент окончания ожидания назначения сеанса соединению). Длительность события равна времени ожидания соединения. Если соединению назначается сеанс, который уже назначен, то текущий поток текущего сое"
			},
			{
				TechJournalAction.xlockTables,
				"Установка исключительной блокировки на таблицу"
			},
			{
				TechJournalAction.xlockTablesShared,
				"Установка разделяемой блокировки на таблицу"
			},
			{
				TechJournalAction.Unknown,
				"Описание отсутствует"
			},
			{
				TechJournalAction.None,
				"Нет действия"
			}
		};

		public static string GetDescription(this TechJournalAction action)
		{
			if (_actionDescription.TryGetValue(action, out string Description))
				return Description;
			else
				return _actionDescription[TechJournalAction.Unknown];
		}

		public static TechJournalAction Parse(string actionTypeName)
		{
			if (string.IsNullOrEmpty(actionTypeName))
				return TechJournalAction.None;
			else if (Enum.TryParse(actionTypeName, true, out TechJournalAction enumOut))
				return enumOut;
			else
				return TechJournalAction.Unknown;
		}
	}
}