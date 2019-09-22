using System;
using System.Linq;
using Calender.Domain.Commands;
using Dapper;

namespace Calender.Data
{
    using static ConnectionFunctions;

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
                    new { @event.Id, @event.Title, @event.Description, @event.When, @event.End }));
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

        public Event Get(Guid id)
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
                        new Event(
                            id: record.Id,
                            title: record.Title,
                            description: record.Description,
                            when: record.When,
                            end: record.End))
                    .FirstOrDefault());
        }
    }
}
