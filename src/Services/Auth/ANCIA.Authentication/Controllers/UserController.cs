using ANCIA.ApiCore.Controllers;
using ANCIA.Authentication.Application.Commands;
using ANCIA.Core.Core;
using ANCIA.Core.Messages.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ANCIA.Authentication.Controllers
{
    [Route("api/users")]
    public class UserController : MainController
    {
        private IMediatorHandler _mediatorHandler;

        public UserController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            ProcessResult<string> processResult = await _mediatorHandler.SendCommand(createUserCommand);
            if (processResult.HasError())
            {
                var result = CreateErrorReturn(
                    getErrorList(processResult.Errors));
                return BadRequest(result);
            }
            return Ok();
        }
    }
}
