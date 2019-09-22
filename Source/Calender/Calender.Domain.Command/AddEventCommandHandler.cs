using System;
using Calender.Domain.ValueObjects;

namespace Calender.Domain.Commands
{
    public class AddEventCommandHandler : ICommandHandler<AddEventCommand>
    {
        readonly IEventRepository repository;

        public AddEventCommandHandler(IEventRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(AddEventCommand command)
        {
            // validation
            if (string.IsNullOrEmpty(command.Title))
                throw new ValidationException(Error.InvalidTitle);

            if (command.When <= DateTime.Now)
                throw new ValidationException(Error.DateInPast);

            if (command.End.Hour - command.When.Hour < 1)
                throw new ValidationException(Error.InvalidLength);            

            if (command.When.Date != command.End.Date)
                throw new ValidationException(Error.DateInPast); // event spreads to next day

            if (this.repository.EventExistsAtThisHour(command.When)
                || this.repository.EventExistsAtThisHour(command.End))
            {
                throw new ValidationException(Error.HourAlreadyHasEvent);
            }

            // assume data is valid and force option creation, when rewritten using validation
            // this won't be necessary
            var @event = Event.Create(
                Guid.NewGuid(),
                Subject.Create(
                    String100.Of(command.Title).ValueUnsafe(),
                    String1000.Of(command.Description)),
                Interval.Create(
                    Moment.Of(command.When).ValueUnsafe(),
                    Moment.Of(command.End).ValueUnsafe()).ValueUnsafe());

            this.repository.Add(@event);
        }
    }
}
