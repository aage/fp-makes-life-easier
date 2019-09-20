using System;
using System.Collections.Generic;

namespace Calender.Domain.Queries
{
    public interface IEventsQuery
    {
        IEnumerable<EventSummaryModel> Get();
    }
}
