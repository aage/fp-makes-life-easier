using System;
using System.Collections.Generic;
using System.Linq;
using Calender.Domain.Commands;
using Calender.Domain.ValueObjects;
using Dapper;
using LaYumba.Functional;

namespace Calender.Data
{
    using static ConnectionFunctions;

    public static class EventFunctions
    {
        public static Action<ConnectionString, Event> Add =
        (connStr, @event) => 
            Connect(connStr,
                conn =>
                conn.Execute(@"
                    INSERT INTO [dbo].[Events] (Id, Title, Description, [When], [End])
                    VALUES (@Id, @Title, @Description, @When, @End)",
                    new
                    {
                        Id = @event.Id,
                        Title = @event.Subject.Title.Value.ToString(),
                        Description = @event.Subject.Description.Match(() => null, (d) => d.Value),
                        When = @event.Interval.Start.Value,
                        End = @event.Interval.End.Value
                    }));

        public static Action<ConnectionString, Event> Delete =>
            (connStr, @event) => Connect(connStr,
                conn =>
                conn.Execute(@"
                    DELETE
                    FROM [dbo].[Events]
                    WHERE Id = @id",
                    new { id = @event.Id }));

        public static Option<Event> Get(ConnectionString connStr, Guid id)
        {
            return Connect(connStr,
                conn => conn
                    .Query(@"
                        SELECT TOP 1
                            Id,
                            Title,
                            Description,
                            [When],
                            [End]
                        FROM [dbo].[Events]
                        WHERE Id = @id",
                        new { id })
                    .Select(record =>
                    {
                        // assume all data is correct and force options creation
                        var title = String100.Of((string)record.Title).ValueUnsafe();
                        var descriptionOption = String1000.Of((string)record.Description);
                        var subject = Subject.Create(title, descriptionOption);

                        var when = Moment.Of((DateTime)record.When).ValueUnsafe();
                        var end = Moment.Of((DateTime)record.End).ValueUnsafe();
                        var interval = Interval.Create(when, end).ValueUnsafe();

                        var @event = Event.Create(id, subject, interval);
                        return @event;
                    })
                    .FirstOrDefault());
        }

        public static IEnumerable<Event> GetForDay(ConnectionString connStr, DateTime thisDay)
        {
            // avoid sql breakage
            thisDay = thisDay == default ? new DateTime(1900, 1, 1) : thisDay;

            return Connect(connStr,
                conn => conn
                    .Query(@"
                        SELECT
                            Id,
                            Title,
                            Description,
                            [When],
                            [End]
                        FROM [dbo].[Events]
                        WHERE DATEDIFF(day, [When], @thisDay) = 0",
                        new { thisDay })
                    .Select(record =>
                    {
                        // assume all data is correct and force options creation
                        var title = String100.Of((string)record.Title).ValueUnsafe();
                        var descriptionOption = String1000.Of((string)record.Description);
                        var subject = Subject.Create(title, descriptionOption);

                        var when = Moment.Of((DateTime)record.When).ValueUnsafe();
                        var end = Moment.Of((DateTime)record.End).ValueUnsafe();
                        var interval = Interval.Create(when, end).ValueUnsafe();

                        Guid id = record.Id;
                        var @event = Event.Create(id, subject, interval);
                        return @event;
                    })
                    .ToList());
        }
    }
}
