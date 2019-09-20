using System;

namespace Calender.Domain.Queries
{
    public class EventSummaryModel
    {
        public Guid Id { get; set; }
        public string Title { get; }
        public DateTime When { get; }
        public DateTime End { get; }
    }
}
