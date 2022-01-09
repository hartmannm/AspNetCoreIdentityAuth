using ANCIA.Core.Messages.Commands;

namespace ANCIA.Core.Messages.Mediator
{
    public interface IMediatorHandler
    {
        Task<CommandResult> SendCommand<T>(T command) where T : Command;
    }
}
