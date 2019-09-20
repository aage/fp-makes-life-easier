using System;
using Calender.Domain.Queries;

namespace Calender.Data
{
    public interface IEventQuery
    {
        EventModel Get(Guid id);
    }
}
