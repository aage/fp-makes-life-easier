using System;
using LaYumba.Functional;

namespace Calender.Domain.ValueObjects
{
    using static F;

    public struct Interval
    {
        private Interval(Moment start, Moment end)
        {
            this.Start = start;
            this.End = end;
        }

        public Moment Start { get; }
        public Moment End { get; }

        public static Func<Moment, Moment, Option<Interval>> Create =
            (start, end) => AreValid(start, end) ? Some(new Interval(start, end)) : None;

        static bool AreValid(Moment start, Moment end)
        {
            DateTime s = start;
            DateTime e = end;

            bool atLeastOneHour = e.Hour - s.Hour > 0;
            bool sameDay = s.Date == e.Date;
            return sameDay && atLeastOneHour;
        }
    }
}
