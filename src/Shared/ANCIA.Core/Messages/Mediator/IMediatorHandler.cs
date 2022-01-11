using ANCIA.Core.Messages.Commands;

namespace ANCIA.Core.Messages.Mediator
{
    public interface IMediatorHandler
    {
        Task<T> SendCommand<T>(Command<T> command);
    }
}
