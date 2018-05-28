using BuildingBlock.Core.Notifications;

namespace BuildingBlock.Core.Commands
{
    public interface ICommandHandler<in TCommand, out TCommandResult>: INotifiable
        where TCommand : ICommand
    {
        TCommandResult Handle(TCommand command);
    }
}
