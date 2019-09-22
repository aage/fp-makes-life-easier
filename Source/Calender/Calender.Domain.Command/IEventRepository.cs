using System;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    public interface IEventRepository
    {
        Option<Event> Get(Guid id);
        void Add(Event @event);
        void Delete(Event @event);
        bool EventExistsAtThisHour(DateTime when);
    }
}
