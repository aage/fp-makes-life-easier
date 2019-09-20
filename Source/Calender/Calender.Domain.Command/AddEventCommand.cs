using System;

namespace Calender.Domain.Commands
{
    public class AddEventCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public DateTime End { get; set; }
    }
}
