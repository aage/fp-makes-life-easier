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

    public static class IntervalExt
    {
        public static Validation<Interval> ValidInterval
            (DateTime start, DateTime end)
        {
            var startVal = Moment.Of(start).ToValidation(() => Errors.InvalidMoment);
            var endVal = Moment.Of(end).ToValidation(() => Errors.InvalidMoment);

            var intervalVal = Valid(Interval.Create)
                .Apply(startVal)
                .Apply(endVal);

            // find out whether moments or interval are invalid
            return intervalVal.Match(
                (errors) => Invalid(errors), // wrap errors in invalid
                (opt) => opt.Match( // moments were valid -> check if option is some
                    () => Invalid(Errors.InvalidInterval), // option is none, this means bad interval
                    (interval) => Valid(interval))); // everything good -> re-wrap as valid
        }

        public static Func<Interval, Interval, bool> IntervalsOverlap =
            (left, right) =>
            {
                // taken from: https://stackoverflow.com/a/13513973/2877982
                bool overlap = left.Start < right.End && right.Start < left.End;
                return overlap;
            };
    }
}
