using System;
using LaYumba.Functional;

namespace Calender.Domain.ValueObjects
{
    using static F;

    /// <summary>
    /// Represents a data of one day.
    /// </summary>
    public struct Date
    {
        private Date(DateTime value)
        {
            this.Value = value.Date;
        }

        public DateTime Value { get; }

        public static Func<DateTime, Option<Date>> Of =
            (dateTime) => dateTime != default
                ? Some(new Date(dateTime))
                : None;
    }
}
