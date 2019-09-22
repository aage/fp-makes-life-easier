using LaYumba.Functional;

namespace Calender.Domain.ValueObjects
{
    public static class Errors
    {
        public static Error InvalidMoment => new InvalidMomentError();
        public static Error InvalidInterval => new InvalidIntervalError();

        private sealed class InvalidMomentError : Error
        { public override string Message => "A datetime cannot be a default datetime."; }

        private sealed class InvalidIntervalError : Error
        {
            public override string Message =>
                  "An end datetime must be at least one hour after the start date" +
                  " and cannot span over multiple days";
        }
    }
}
