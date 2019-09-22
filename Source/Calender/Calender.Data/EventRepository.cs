using System;
using System.Linq;
using Calender.Domain.Commands;
using Calender.Domain.ValueObjects;
using Dapper;
using LaYumba.Functional;

namespace Calender.Data
{
    using static ConnectionFunctions;
    using static OptionFunctions;

    public class EventRepository : IEventRepository
    {
        readonly ConnectionString connStr;

        public EventRepository(ConnectionString connStr)
        {
            this.connStr = connStr;
        }

        public void Add(Event @event)
        {
            Connect(this.connStr,
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
        }

        public void Delete(Event @event)
        {
            Connect(this.connStr,
                conn =>
                conn.Execute(@"
                    DELETE
                    FROM [dbo].[Events]
                    WHERE Id = @id",
                    new { id = @event.Id }));
        }

        public bool EventExistsAtThisHour(DateTime when)
        {
            return Connect(this.connStr,
                conn => conn
                    .Query<int>(@"
                        SELECT COUNT(1)
                        FROM [dbo].[Events]
                        WHERE (@when BETWEEN [When] AND [End])
                            OR (@when BETWEEN [When] AND [End])",
                        new { when })
                    .First() == 1);
        }

        public Option<Event> Get(Guid id)
        {
            return Connect(this.connStr,
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
    }
}
