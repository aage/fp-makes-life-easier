using System;

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

            var @event = new Event(
                Guid.NewGuid(),
                command.Title,
                command.Description,
                command.When,
                command.End);

            this.repository.Add(@event);
        }
    }
}
