using System;

namespace Calender.Domain.Commands
{
    public enum Error
    {
        InvalidTitle = 1,
        InvalidDescription,
        DateInPast,
        EventOverMultipleDays,
        HourAlreadyHasEvent,
        InvalidLength,
        EventDoesNotExist
    }

    public class ValidationException : Exception
    {
        internal ValidationException(Error error)
            : base(error.ToString())
        {
            this.Error = error;
        }

        public Error Error { get; }

        public override string ToString() => this.Error.ToString();
    }
}
