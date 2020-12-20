using System;
using System.Collections.Generic;
using System.Text;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant
{
    public interface IEventData
    {
        string GetCustomFieldsAsJSON();
        T AsEvent<T>() where T : EventData, IEventData;
    }
}
