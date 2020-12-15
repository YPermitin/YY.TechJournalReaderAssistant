using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant
{
    internal static class LogParserTechJournal
    {
        #region Public Static Methods

        public static bool ItsBeginOfEvent(string sourceString)
        {
            return sourceString != null && Regex.IsMatch(sourceString, @"^\d\d:\d\d.\d\d\d\d(\d\d)?-");
        }
        public static bool ItsEndOfEvent(StreamReader stream, string sourceString)
        {
            long previousStreamPosition = stream.GetPosition();
            string nextString = stream.ReadLineWithoutNull();
            stream.SetPosition(previousStreamPosition);

            if (ItsBeginOfEvent(nextString))
                return true;
            else
                return false;
        }
        public static EventData Parse(string originEventSource, string currentFile)
        {
            string bufferEventSource = String.Copy(originEventSource);
            EventData dataRow = new EventData();
            FileInfo currentFileInfo = new FileInfo(currentFile);
            string dateFromFileAsString = currentFileInfo.Name.Replace(".log", string.Empty);

            int indexEndOfDate = bufferEventSource.IndexOf('-');
            string periodAsString = bufferEventSource.Substring(0, indexEndOfDate);
            long periodMilliseconds = long.Parse(periodAsString.Substring(6, 3));

            dataRow.Period = new DateTime(
                2000 + int.Parse(dateFromFileAsString.Substring(0, 2)),
                int.Parse(dateFromFileAsString.Substring(2, 2)),
                int.Parse(dateFromFileAsString.Substring(4, 2)),
                int.Parse(dateFromFileAsString.Substring(6, 2)),
                int.Parse(periodAsString.Substring(0, 2)),
                int.Parse(periodAsString.Substring(3, 2))
            ).AddMilliseconds(periodMilliseconds);

            bool isFormat83 = periodAsString.Length == 12;
            if (isFormat83)
                dataRow.PeriodMoment = long.Parse(periodAsString.Substring(6, 6));
            else
                dataRow.PeriodMoment = long.Parse(periodAsString.Substring(6, 4)) * 100;

            int indexEndOfDuration = bufferEventSource.IndexOf(',');
            string durationAsString = bufferEventSource.Substring(indexEndOfDate + 1, indexEndOfDuration - indexEndOfDate - 1);
            dataRow.Duration = long.Parse(durationAsString) * (isFormat83 ? 10 : 100);

            bufferEventSource = bufferEventSource.Substring(indexEndOfDuration + 1, bufferEventSource.Length - indexEndOfDuration - 1);
            int indexEndOfEventName = bufferEventSource.IndexOf(',');
            dataRow.EventName = bufferEventSource.Substring(0, indexEndOfEventName);

            bufferEventSource = bufferEventSource.Substring(indexEndOfEventName + 1, bufferEventSource.Length - indexEndOfEventName - 1);
            int indexEndOfLevel = bufferEventSource.IndexOf(',');
            dataRow.Level = int.Parse(bufferEventSource.Substring(0, indexEndOfLevel));

            bufferEventSource = bufferEventSource.Substring(indexEndOfLevel + 1, bufferEventSource.Length - indexEndOfLevel - 1);
            int indexOfDelimeter = bufferEventSource.IndexOf("=", StringComparison.InvariantCulture);

            bufferEventSource = bufferEventSource.Replace("''", "¦");
            bufferEventSource = bufferEventSource.Replace(@"""""", "÷");

            while (indexOfDelimeter > 0)
            {
                string paramName = bufferEventSource.Substring(0, indexOfDelimeter);
                string valueAsString = string.Empty;

                bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1);
                if (!string.IsNullOrEmpty(bufferEventSource))
                {
                    if (bufferEventSource.Substring(0, 1) == "'")
                    {
                        bufferEventSource = bufferEventSource.Substring(1);
                        indexOfDelimeter = bufferEventSource.IndexOf("'", StringComparison.InvariantCulture);
                        if (indexOfDelimeter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimeter).Trim();
                            valueAsString = valueAsString.Replace("¦", "'");
                        }
                        if (bufferEventSource.Length > indexOfDelimeter + 1)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1 + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    } else if (bufferEventSource.Substring(0, 1) == "\"")
                    {
                        bufferEventSource = bufferEventSource.Substring(1);
                        indexOfDelimeter = bufferEventSource.IndexOf("\"", StringComparison.InvariantCulture);
                        if (indexOfDelimeter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimeter).Trim();
                            valueAsString = valueAsString.Replace("÷", "\"\"");
                        }
                        if (bufferEventSource.Length > indexOfDelimeter + 1)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1 + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                    else
                    {
                        indexOfDelimeter = bufferEventSource.IndexOf(",", StringComparison.Ordinal);
                        if (indexOfDelimeter > 0)
                        {
                            valueAsString = bufferEventSource.Substring(0, indexOfDelimeter).Trim();
                        }
                        else
                        {
                            valueAsString = bufferEventSource;
                        }

                        if (bufferEventSource.Length > indexOfDelimeter)
                        {
                            bufferEventSource = bufferEventSource.Substring(indexOfDelimeter + 1);
                        }
                        else
                        {
                            bufferEventSource = string.Empty;
                        }
                    }
                }

                indexOfDelimeter = bufferEventSource.IndexOf("=", StringComparison.InvariantCulture);

                if (dataRow.Properties.ContainsKey(paramName))
                {
                    int countParamWithSameName = dataRow.Properties.Count(e => e.Key == paramName);
                    dataRow.Properties.Add($"{paramName}#{countParamWithSameName + 1}", valueAsString);
                }
                else
                {
                    dataRow.Properties.Add(paramName, valueAsString);
                }
            }

            return dataRow;
        }

        #endregion
    }
}
