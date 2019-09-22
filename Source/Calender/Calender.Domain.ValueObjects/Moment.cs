using System;
using LaYumba.Functional;

namespace Calender.Domain.ValueObjects
{
    using static F;

    // a moment is an non-default datetime without minutes (hours only)
    public struct Moment
    {
        private Moment(DateTime value)
        {
            this.Value = value;
        }

        public DateTime Value { get; }

        public static Func<DateTime, Option<Moment>> Of =
            (value) => IsValid(value) 
                ? Some(new Moment(value.WithoutMinutes()))
                : None;

        static bool IsValid(DateTime value) => value != default;

        public static implicit operator DateTime(Moment m) => m.Value;
    }

    internal static class DateTimeExt
    {
        // copies the datetime without the minutes
        internal static DateTime WithoutMinutes
            (this DateTime d)
            => new DateTime(d.Year, d.Month, d.Day, d.Hour, 0, 0);
    }
}
