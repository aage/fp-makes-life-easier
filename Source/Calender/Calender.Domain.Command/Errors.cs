using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    public static class Errors
    {
        public static Error InvalidTitle => new InvalidTitleError();
        public static Error InvalidDescription => new InvalidDescriptionError();
        public static Error EventInPast => new EventInPastError();
        public static Error EventDoesNotExist => new EventDoesNotExistError();
        public static Error EventOverlaps => new EventOverlapsError();

        private sealed class InvalidTitleError : Error
        { public override string Message => "A title is mandatory and can only be 100 chars."; }

        private sealed class InvalidDescriptionError : Error
        { public override string Message => "A description is optional but can only be 1000 chars."; }

        private sealed class EventInPastError : Error
        { public override string Message => "An event cannot take place in the past"; }

        private sealed class EventDoesNotExistError : Error
        { public override string Message => "A requested event does not exist"; }
        private sealed class EventOverlapsError : Error
        { public override string Message => "An event cannot overlap with another event"; }
    }
}
