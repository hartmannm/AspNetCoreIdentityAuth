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

        public async Task<T> SendCommand<T>(Command<T> command)
        {
            return await _mediator.Send(command);
        }
    }
}
