using LaYumba.Functional;

namespace Calender.Domain.Commands
{
    public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand>
    {
        readonly IEventRepository repository;

        public DeleteEventCommandHandler(IEventRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(DeleteEventCommand command)
        {
            var eventOption = this.repository.Get(command.Id);
            eventOption.Match(
                () => throw new ValidationException(Error.EventDoesNotExist),
                (@event) => this.repository.Delete(@event));
        }
    }
}
