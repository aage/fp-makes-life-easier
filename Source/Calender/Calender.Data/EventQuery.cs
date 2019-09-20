using System;
using System.Data.SqlClient;
using System.Linq;
using Calender.Domain.Queries;
using Dapper;

namespace Calender.Data
{
    public class EventQuery : IEventQuery
    {
        readonly string connectionString;

        public EventQuery(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public EventModel Get(Guid id)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                return conn
                    .Query<EventModel>(@"
                        SELECT TOP 1
                            Id,
                            Title,
                            Description,
                            [When],
                            [End]
                        FROM [dbo].[Events]
                        WHERE Id = @id",
                        new { id })
                    .FirstOrDefault();
            }
        }
    }
}
