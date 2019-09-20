namespace Calender.Domain.Commands
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}
