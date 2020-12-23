using System;
using System.Collections.Generic;
using System.Text;

namespace YY.TechJournalReaderAssistant.Helpers
{
    internal static class DictionaryExtensions
    {
        public static string GetStringValueByKey(this Dictionary<string, string> props, string name)
        {
            return props.ContainsKey(name) ? props[name] : null;
        }
        public static long? GetLongValueByKey(this Dictionary<string, string> props, string name)
        {
            if (props.ContainsKey(name) && long.TryParse(props[name], out var longValue))
                return longValue;
            return null;
        }
        public static bool? GetBoolValueByKey(this Dictionary<string, string> props, string name)
        {
            if (props.ContainsKey(name) && bool.TryParse(props[name], out var boolValue))
                return boolValue;
            return null;
        }
    }
}
