using System;
using System.Collections.Generic;
using System.Linq;
using Calender.Domain.ValueObjects;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    using Date = ValueObjects.Date;
    using static F;
    using static IntervalExt;

    public class Day
    {
        private Day(Date date, IEnumerable<Event> events)
        {
            this.Date = date;
            this.Events = events;
        }

        public Date Date { get; }
        public IEnumerable<Event> Events { get; }

        public static Func<IEnumerable<Event>, Date, Day> Of =
            (events, date) => new Day(date, events);
    }

    public static class DayExt
    {
        public static Func<Day, Event, Validation<Event>> AddEventToDay =
            (day, @event) =>
            {
                // check if day already contains an event at that time
                return day.Events
                        .Select(ev => ev.Interval)
                        .Any(interval => IntervalsOverlap(interval, @event.Interval))
                    ? Invalid(Errors.EventOverlaps)
                    : Valid(@event);
            };
    }
}