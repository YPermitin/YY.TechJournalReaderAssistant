using System.IO;
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
        public static EventData Parse(string originEventSource, string currentFile, long eventId)
        {
            return EventData.Create(originEventSource, currentFile, eventId);
        }

        #endregion
    }
}
