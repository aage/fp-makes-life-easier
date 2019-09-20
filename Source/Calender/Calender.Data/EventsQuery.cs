using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Calender.Domain.Queries;
using Dapper;

namespace Calender.Data
{
    public class EventsQuery : IEventsQuery
    {
        readonly string connectionString;

        public EventsQuery(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<EventSummaryModel> Get()
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                return conn
                    .Query<EventSummaryModel>(@"
                        SELECT
                            Id,
                            Title,
                            [When],
                            [End]
                        FROM [dbo].[Events]")
                    .ToList();
            }
        }
    }
}
