using ANCIA.ApiCore.Controllers;
using ANCIA.Authentication.Application.Commands;
using ANCIA.Core.Core;
using ANCIA.Core.Messages.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANCIA.Authentication.Controllers
{
    [Route("api/users")]
    public class UserController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UserController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            ProcessResult<string> processResult = await _mediatorHandler.SendCommand(createUserCommand);
            if (processResult.HasError())
            {
                var result = CreateErrorReturn(
                    getErrorList(processResult.Errors));
                return BadRequest(result);
            }
            return Ok(CreateSuccessReturn(processResult.Content));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteUser")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUserComand { UserId = id };
            ProcessResult<string> processResult = await _mediatorHandler.SendCommand(command);
            if (processResult.HasError())
            {
                var result = CreateErrorReturn(
                    getErrorList(processResult.Errors));
                return BadRequest(result);
            }
            return Ok(CreateSuccessReturn(processResult.Content));
        }

        [HttpPost("active/{id}")]
        [Authorize(Policy = "UpdateUser")]
        public async Task<IActionResult> Activate(string id)
        {
            var command = new ActivateUserCommand { UserId = id };
            ProcessResult<string> processResult = await _mediatorHandler.SendCommand(command);
            if (processResult.HasError())
            {
                var result = CreateErrorReturn(
                    getErrorList(processResult.Errors));
                return BadRequest(result);
            }
            return Ok(CreateSuccessReturn(processResult.Content));
        }
    }
}
