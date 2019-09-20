using System;

namespace Calender.Domain.Commands
{
    public class Event
    {
        public Event(Guid id,
            string title,
            string description, // optional
            DateTime when,
            DateTime end)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.When = when;
            this.End = end;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public DateTime End { get; set; }
    }
}
