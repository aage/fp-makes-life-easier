using System;
using System.Linq;
using Calender.Data;
using Calender.Domain.Commands;
using LaYumba.Functional;

namespace Calender.Api.IoC
{
    using static ConnectionFunctions;
    using static EventFunctions;

    public static class AddEventIoc
    {
        public static Func<AddEventCommand, Validation<Event>> WireUpAddEvent
            (ConnectionString connStr)
        {
            // Gather all necessary data and give to workflow.
            return cmd =>
            {
                var workflow = SetupWorkflow(connStr, cmd);
                var persist = Add.Apply(connStr);

                var result = workflow(cmd).Do(persist); // persist when valid

                return result;
            };
        }

        private static Func<AddEventCommand, Validation<Event>> 
            SetupWorkflow(ConnectionString connStr, AddEventCommand cmd)
        {
            var eventsForDay = GetForDay(connStr, cmd.When);
            var workflow = AddEvent.BuildWorkflow(
                Guid.NewGuid(),
                eventsForDay);
            return workflow;
        }
    }
}
