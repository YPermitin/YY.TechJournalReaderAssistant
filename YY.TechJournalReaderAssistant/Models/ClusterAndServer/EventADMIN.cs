using YY.TechJournalReaderAssistant.Helpers;

namespace YY.TechJournalReaderAssistant.Models.ClusterAndServer
{
    public class EventADMIN : EventData
    {
        public bool? AddInUseFullAccess => Properties.GetBoolValueByKey("ADDINUSEFULLACCESS");
        public string Administrator => Properties.GetStringValueByKey("ADMINISTRATOR");
        public string ApplicationExt => Properties.GetStringValueByKey("APPLICATIONEXT");
        public bool? AllModulesExtension => Properties.GetBoolValueByKey("ALLMODULESEXTENSION");
        public bool? AllowedRead => Properties.GetBoolValueByKey("ALLOWEDREAD");
        public bool? AllowedWrite => Properties.GetBoolValueByKey("ALLOWEDWRITE");
        public long? CentralPort => Properties.GetLongValueByKey("ALLOWEDWRITE");
        public long? CentralServerPort => Properties.GetLongValueByKey("CENTRALSERVER");
        public long? ClusterPort => Properties.GetLongValueByKey("CLUSTER");
        public string ClusterID => Properties.GetStringValueByKey("CLUSTERID");
        public string CMD => Properties.GetStringValueByKey("CMD");
        public long? ConnectionDenied => Properties.GetLongValueByKey("CONNDN");
        public long? ConnectionNumber => Properties.GetLongValueByKey("CONNECTIONNUMBER");
        public string ConnectionID => Properties.GetStringValueByKey("CNNECTIONID");
        public bool? ComUseFullAccess => Properties.GetBoolValueByKey("COMUSEFULLACCESS");
        public bool? CryptographyAllowed => Properties.GetBoolValueByKey("CRYPTOGRAPHYALLOWED");
        public string DB => Properties.GetStringValueByKey("DB");
        public string DBServer => Properties.GetStringValueByKey("DBSRVR");
        public string DBUID => Properties.GetStringValueByKey("DBUID");
        public long? DedicateAll => Properties.GetLongValueByKey("DEDICATEALL");
        public string ExceptionDescription => Properties.GetStringValueByKey("DESCR");
        public long? DeniedAccessCode => Properties.GetLongValueByKey("DNALCD");
        public long? DeniedSessionBegin => Properties.GetLongValueByKey("DNFROM");
        public string DeniedMessage => Properties.GetStringValueByKey("DNMSG");
        public string DeniedParams => Properties.GetStringValueByKey("DNPRM");
        public long? DeniedSessionEnd => Properties.GetLongValueByKey("DNTO");
        public long? ErrorsCountThreshold => Properties.GetLongValueByKey("ERRORSCOUNTTHRESHOLD");
        public bool? ESMEn => Properties.GetBoolValueByKey("ESMEN");
        public string ESMURL => Properties.GetStringValueByKey("ESMURL");
        public long? ExpirationTimeoutWorkProcess => Properties.GetLongValueByKey("EXPIRATIONTIMEOUT");
        public bool? ExternalAppFullAccess => Properties.GetBoolValueByKey("EXTERNALAPPFULLACCESS");
        public string FileName => Properties.GetStringValueByKey("FILENAME");
        public bool? FileSystemFullAccess => Properties.GetBoolValueByKey("FILESYSTEMFULLACCESS");
        public long? FaultToleranceLevel => Properties.GetLongValueByKey("FAULTTOLERANCELEVEL");
        public long? ExternalModuleHash => Properties.GetLongValueByKey("HASH");
        public string Host => Properties.GetStringValueByKey("HOST");
        public string SecurityProfile => Properties.GetStringValueByKey("IBSEPR");
        public string InfoBaseID => Properties.GetStringValueByKey("INFOBASEID");
        public bool? InternetUseFullAccess => Properties.GetBoolValueByKey("INTERNETUSEFULLACCESS");
        public long? KillProblemProcesses => Properties.GetLongValueByKey("KILLPROBLEMPROCESSES");
        public long? LifeTimeLimitWorkProcess => Properties.GetLongValueByKey("LIFETIMELIMIT");
        public bool? LicDstr => Properties.GetBoolValueByKey("LICDSTR");
        public long? LoadBalancingMode => Properties.GetLongValueByKey("LOADBALANCINGMODE");
        public string Locale => Properties.GetStringValueByKey("LOCALE");
        public long? MaxMemorySize => Properties.GetLongValueByKey("MAXMEMORYSIZE");
        public long? MaxMemoryTimeLimit => Properties.GetLongValueByKey("MAXMEMORYTIMELIMIT");
        public long? MaxRPIBConnections => Properties.GetLongValueByKey("MAXRPIBCONNECTIONS");
        public long? MaxRPIBQuantity => Properties.GetLongValueByKey("MAXRPIBQUANTITY");
        public long? Mode => Properties.GetLongValueByKey("MODE");
        public string ModulesAvailableForExtension => Properties.GetStringValueByKey("MODULESAVAILABLEFOREXTENSION");
        public string ModulesNotAvailableForExtension => Properties.GetStringValueByKey("MODULESNOTAVAILABLEFOREXTENSION");
        public string Profile => Properties.GetStringValueByKey("PROFILE"); 
        public string ObjectType => Properties.GetStringValueByKey("OBJECTTYPE");
        public long? PID => Properties.GetLongValueByKey("PID");
        public string PhysicalPath => Properties.GetStringValueByKey("PHYSICALPATH");
        public long? MainPort => Properties.GetLongValueByKey("PORT");
        public long? Priority => Properties.GetLongValueByKey("PRIORITY");
        public bool? PrivilegedModeInSafeModeAllowed => Properties.GetBoolValueByKey("PRIVILEGEDMODEINSAFEMODEALLOWED");
        public string ProcessID => Properties.GetStringValueByKey("PROCESSID");
        public string Protocol => Properties.GetStringValueByKey("PROTOCOL");
        public string PortRanges => Properties.GetStringValueByKey("RANGES");
        public string RefInfobase => Properties.GetStringValueByKey("REF");
        public bool? RightExtension => Properties.GetBoolValueByKey("RIGHTEXTENSION");
        public bool? RightExtensionDefinitionRoles => Properties.GetBoolValueByKey("RIGHTEXTENSIONDEFINITIONROLES");
        public string RuleID => Properties.GetStringValueByKey("RULEID");
        public string RuleType => Properties.GetStringValueByKey("RULETYPE");
        public long? SafeCallMemoryLimit => Properties.GetLongValueByKey("SAFECALLMEMORYLIMIT");
        public long? SafeWorkingProcessesMemoryLimit => Properties.GetLongValueByKey("SAFEWORKINGPROCESSESMEMORYLIMIT");
        public bool? SafeModeProfile => Properties.GetBoolValueByKey("SAFEMODEPROFILE");
        public bool? ScheduleJobDenied => Properties.GetBoolValueByKey("SCHJOBDN");
        public string SeanceID => Properties.GetStringValueByKey("SEANCEIS");
        public string SessionID => Properties.GetStringValueByKey("SESSIONID");
        public string ServerName => Properties.GetStringValueByKey("SERVERNAME");
        public string HTTPStatus => Properties.GetStringValueByKey("STATUS");
        public long? SQLYearOffset => Properties.GetLongValueByKey("SQLYOFFS");
        public long? SecurityConnectionLevel => Properties.GetLongValueByKey("SLEV");
        public long? SoftRPMemoryLimit => Properties.GetLongValueByKey("SOFTRPMEMORYLIMIT");
        public string OperationSystem => Properties.GetStringValueByKey("SYSTEM");
        public bool? UnsafeExternalModuleFullAccess => Properties.GetBoolValueByKey("UNSAFEEXTERNALMODULEFULLACCESS");
        public string UUID => Properties.GetStringValueByKey("UUID");
        public string ParamValue => Properties.GetStringValueByKey("VAL");
    }
}