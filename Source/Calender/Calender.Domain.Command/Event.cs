using System;
using Calender.Domain.ValueObjects;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    using static F;
    using static SubjectExt;
    using static IntervalExt;

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

    public static class EventExt
    {
        public static Validation<Event> ValidEvent
            (Guid id, string title, string description, DateTime when, DateTime end)
        {
            var subjectVal = ValidSubject(title, description);
            var intervalVal = ValidInterval(when, end);

            var eventVal = Valid(Event.Create)
                .Apply(id) // id is going to be created internally, so no validation
                .Apply(subjectVal)
                .Apply(intervalVal);

            return eventVal;
        }
    }
}
