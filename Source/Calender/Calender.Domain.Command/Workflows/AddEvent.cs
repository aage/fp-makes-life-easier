using System;
using System.Collections.Generic;
using Calender.Domain.ValueObjects;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    using static DayExt;
    using static EventExt;
    using Date = ValueObjects.Date;

    public static class AddEvent
    {
        public static Func<AddEventCommand, Validation<Event>> BuildWorkflow
            (Guid id, Moment noEarlierThan, IEnumerable<Event> eventsOfDay)
        {
            return cmd =>
            {
                // get context of day
                var dayVal = Date.Of(cmd.When)
                    .Map(Day.Of.Apply(eventsOfDay))
                    .ToValidation(() => Errors.EventInPast);

                var eventVal = ValidEvent(id, cmd.Title, cmd.Description, cmd.When, cmd.End);
                eventVal = dayVal.Bind(d =>
                    {
                        // find out whether day already holds event at this time
                        return eventVal.Bind(e => AddEventToDay(d, e));
                    });

                return eventVal;
            };
        }
    }
}
