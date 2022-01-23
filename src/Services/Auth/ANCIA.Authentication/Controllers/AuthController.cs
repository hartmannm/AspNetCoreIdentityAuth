using ANCIA.ApiCore.Controllers;
using ANCIA.Authentication.Application.Commands;
using ANCIA.Core.Messages.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANCIA.Authentication.Controllers
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AuthController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var result = await _mediatorHandler.SendCommand(loginCommand);
            if (result.HasError())
                return BadRequest(CreateErrorReturn(getErrorList(result.Errors)));
            return Ok(CreateSuccessReturn(result.Content));
        }
    }
}
