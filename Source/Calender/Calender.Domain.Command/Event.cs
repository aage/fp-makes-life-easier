using System;
using Calender.Domain.ValueObjects;

namespace Calender.Domain.Commands
{
    public class Event
    {
        private Event(Guid id,
            Subject subject,
            Interval interval)
        {
            this.Id = id;
            this.Subject = subject;
            this.Interval = interval;
        }

        public Guid Id { get; }
        public Subject Subject { get; }
        public Interval Interval { get; }

        public static Func<
            Guid, Subject, Interval,
            Event> Create =
            (id, subject, interval) => new Event(id, subject, interval);
    }
}
