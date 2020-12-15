using System;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant.EventArguments
{
    public sealed class AfterReadEventArgs : EventArgs
    {
        public AfterReadEventArgs(EventData rowData, long eventNumber)
        {
            EventData = rowData;
            EventNumber = eventNumber;
        }

        public EventData EventData { get; }
        public long EventNumber { get; }
    }    
}
