using System;
using Calender.Domain.ValueObjects;
using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    using static F;

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

    public static class SubjectExt
    {
        public static Validation<Subject> ValidSubject
            (string title, string description)
        {
            var titleVal = String100.Of(title).ToValidation(() => Errors.InvalidTitle);
            var descriptionVal = String1000.Of(description);

            var subjectVal = Valid(Subject.Create)
                .Apply(titleVal)
                .Apply(descriptionVal);

            return subjectVal;
        }
    }
}
