using ANCIA.Core.Messages.Commands;
using MediatR;

namespace ANCIA.Core.Messages.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResult> SendCommand<T>(T command) where T : Command
        {
            var result = await _mediator.Send(command);
            return result != null ? new CommandResult(result) : CommandResult.Empty();
        }
    }
}
