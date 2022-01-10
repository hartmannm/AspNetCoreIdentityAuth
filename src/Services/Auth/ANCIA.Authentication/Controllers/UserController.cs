using ANCIA.ApiCore.Controllers;
using ANCIA.Authentication.Application.Commands;
using ANCIA.Core.Messages.Mediator;
using ANCIA.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace ANCIA.Authentication.Controllers
{
    [Route("api/users")]
    public class UserController : MainController
    {
        private IMediatorHandler _mediatorHandler;
        private INotifier _notifier;

        public UserController(IMediatorHandler mediatorHandler, INotifier notifier)
        {
            _mediatorHandler = mediatorHandler;
            _notifier = notifier;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            await _mediatorHandler.SendCommand(createUserCommand);
            if (_notifier.HasNotifications())
            {
                var result = CreateErrorReturn(
                    getErrorList(_notifier.GetNotifications()));
                return BadRequest(result);
            }
            return Ok();
        }
    }
}
