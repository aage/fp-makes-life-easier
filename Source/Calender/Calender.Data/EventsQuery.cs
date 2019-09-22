using System.Collections.Generic;
using System.Linq;
using Calender.Domain.Queries;
using Dapper;

namespace Calender.Data
{
    using static ConnectionFunctions;

    public class EventsQuery : IEventsQuery
    {
        readonly ConnectionString connStr;

        public EventsQuery(ConnectionString connStr)
        {
            this.connStr = connStr;
        }

        public IEnumerable<EventSummaryModel> Get()
        {
            return Connect(this.connStr,
                conn =>
                conn.Query<EventSummaryModel>(@"
                        SELECT
                            Id,
                            Title,
                            [When],
                            [End]
                        FROM [dbo].[Events]")
                    .ToList());
        }
    }
}
