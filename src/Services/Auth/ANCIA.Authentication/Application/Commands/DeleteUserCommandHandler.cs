using ANCIA.Authentication.Configuration.Identity;
using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Application.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserComand, ProcessResult<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProcessResult<string>> Handle(DeleteUserComand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return ProcessResult<string>.Fail("Usuário não encontrado");

            user.Delete();
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return ProcessResult<string>.Success(user.Id);

            return ProcessResult<string>.Fail(result.ToNotifications());
        }
    }
}
