﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YY.TechJournalReaderAssistant.Tests.TestData {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ClearSQLQuery {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ClearSQLQuery() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YY.TechJournalReaderAssistant.Tests.TestData.ClearSQLQuery", typeof(ClearSQLQuery).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на (@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbinary(16),@PN varbinary(4),@PN varbinary(4),@PN varbi [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string QueryFromSQLServerExtendedEvents {
            get {
                return ResourceManager.GetString("QueryFromSQLServerExtendedEvents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT
        ///?,
        ///T1._IDRRef
        ///FROM _DocumentChngR15 T1 WITH(NOLOCK)
        ///LEFT OUTER JOIN _Document1 T2 WITH(NOLOCK)
        ///ON T1._IDRRef = T2._IDRRef
        ///WHERE (T1._NodeTRef = ? AND T1._NodeRRef = ?) AND T2._Number IS NULL
        ///UNION ALL SELECT
        ///?,
        ///T3._IDRRef
        ///FROM _DocumentChngR08 T3 WITH(NOLOCK)
        ///LEFT OUTER JOIN _Document2 T4 WITH(NOLOCK)
        ///ON T3._IDRRef = T4._IDRRef
        ///WHERE (T3._NodeTRef = ? AND T3._NodeRRef = ?) AND T4._Number IS NULL
        ///UNION ALL SELECT
        ///?,
        ///T5._IDRRef
        ///FROM _DocumentChngR06 T5 WITH(NOLOCK)
        ///LEFT OUTER JOIN _Do [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string QueryFromTechJournal {
            get {
                return ResourceManager.GetString("QueryFromTechJournal", resourceCulture);
            }
        }
    }
}
