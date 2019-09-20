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
            var @event = this.repository.Get(command.Id);
            if (@event == null)
            {
                throw new ValidationException(Error.EventDoesNotExist);
            }

            this.repository.Delete(@event);
        }
    }
}
