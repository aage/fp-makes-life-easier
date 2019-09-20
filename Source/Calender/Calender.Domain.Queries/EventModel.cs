using System;

namespace Calender.Domain.Queries
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Title { get; }
        public string Description { get; }
        public DateTime When { get; }
        public DateTime End { get; }
    }
}
