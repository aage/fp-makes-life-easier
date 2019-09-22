using System;
using Calender.Domain.Queries;
using LaYumba.Functional;

namespace Calender.Data
{
    public interface IEventQuery
    {
        Option<EventModel> Get(Guid id);
    }
}
