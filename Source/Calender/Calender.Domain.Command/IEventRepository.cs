using System;

namespace Calender.Domain.Commands
{
    public interface IEventRepository
    {
        Event Get(Guid id);
        void Add(Event @event);
        void Delete(Event @event);

        bool EventExistsAtThisHour(DateTime when);
    }
}
