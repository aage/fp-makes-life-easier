using System;
using System.Linq;
using Calender.Domain.Queries;
using Dapper;

namespace Calender.Data
{
    using static ConnectionFunctions;

    public class EventQuery : IEventQuery
    {
        readonly ConnectionString connStr;

        public EventQuery(ConnectionString connStr)
        {
            this.connStr = connStr;
        }

        public EventModel Get(Guid id)
        {
            return Connect(this.connStr,
                conn =>
                conn.Query<EventModel>(@"
                        SELECT TOP 1
                            Id,
                            Title,
                            Description,
                            [When],
                            [End]
                        FROM [dbo].[Events]
                        WHERE Id = @id",
                        new { id })
                    .FirstOrDefault());
        }
    }
}
