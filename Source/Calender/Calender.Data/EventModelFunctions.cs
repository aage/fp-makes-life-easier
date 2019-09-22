using System;
using System.Collections.Generic;
using System.Linq;
using Calender.Domain.Queries;
using Dapper;
using LaYumba.Functional;

namespace Calender.Data
{
    using static ConnectionFunctions;

    public static class EventModelFunctions
    {
        public static Func<ConnectionString, Guid, Option<EventModel>> GetEventModel =
        (connStr, id) =>
            {
                Option<EventModel> option = Connect(connStr,
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
                return option;
            };

        public static Func<ConnectionString, IEnumerable<EventSummaryModel>> GetEventModels =
        (connStr) =>
            {
                return Connect(connStr,
                    conn =>
                    conn.Query<EventSummaryModel>(@"
                            SELECT
                                Id,
                                Title,
                                [When],
                                [End]
                            FROM [dbo].[Events]")
                        .ToList());
            };
    }
}
