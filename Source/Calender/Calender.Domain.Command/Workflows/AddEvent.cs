using System;
using System.Collections.Generic;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    using static DayExt;
    using static EventExt;
    using Date = ValueObjects.Date;

    public static class AddEvent
    {
        public static Func<AddEventCommand, Validation<Event>> BuildWorkflow
            (Guid id, IEnumerable<Event> eventsOfDay)
        {
            return cmd =>
            {
                var dayVal = MapToDay(cmd.When, eventsOfDay);
                var eventVal = MapToEvent(id, cmd);

                // combine both validations to a single result
                eventVal = dayVal.Bind(day =>
                {
                    // partial application returns a new, more specific, function
                    var addToDay = AddEventToDay.Apply(day);
                    return eventVal.Bind(addToDay);
                });

                // this is either valid or invalid (with errors)
                return eventVal;
            };
        }

        static Validation<Day> MapToDay(DateTime when, IEnumerable<Event> eventsOfDay)
        {
            var dayVal = Date.Of(when)
                .Map(Day.Of.Apply(eventsOfDay))
                .ToValidation(() => Errors.EventInPast);
            return dayVal;
        }

        static Validation<Event> MapToEvent(Guid id, AddEventCommand cmd)
        {
            var eventVal = ValidEvent(id, cmd.Title, cmd.Description, cmd.When, cmd.End);
            return eventVal;
        }
    }
}
