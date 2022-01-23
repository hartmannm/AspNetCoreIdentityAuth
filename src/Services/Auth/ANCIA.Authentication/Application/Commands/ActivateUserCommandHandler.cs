using ANCIA.Authentication.Configuration.Identity;
using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Application.Commands
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ProcessResult<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ActivateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProcessResult<string>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return ProcessResult<string>.Fail("Usuário não encontrado");

            user.Activate();
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return ProcessResult<string>.Success(user.Id);

            return ProcessResult<string>.Fail(result.ToNotifications());
        }
    }
}
