using System;
using Calender.Domain.ValueObjects;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    public class Subject
    {
        private Subject(String100 title, Option<String1000> description)
        {
            this.Title = title;
            this.Description = description;
        }

        public String100 Title { get; }
        public Option<String1000> Description { get; }

        public static Func<String100, Option<String1000>, Subject> Create =
            (title, descriptionOption) => new Subject(title, descriptionOption);
    }
}
