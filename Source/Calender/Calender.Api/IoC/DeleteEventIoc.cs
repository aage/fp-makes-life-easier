using System;
using System.Linq;
using Calender.Data;
using Calender.Domain.Commands;
using LaYumba.Functional;

namespace Calender.Api.IoC
{
    using static ConnectionFunctions;
    using static EventFunctions;

    public static class DeleteEventIoc
    {
        public static Func<DeleteEventCommand, Validation<Event>> WireUpDeleteEvent
            (ConnectionString connStr)
        {
            return cmd =>
            {
                var validation = Get(connStr, cmd.Id)
                    .ToValidation(() => Errors.EventDoesNotExist);

                // delete when found
                var delete = Delete.Apply(connStr);
                validation.Do(delete);

                return validation;
            };
        }
    }
}
