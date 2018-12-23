using Assets.Scripts.Commands;

namespace Assets.Scripts.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        void Execute(TCommand command);
    }
}
