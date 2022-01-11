using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Core;
using ANCIA.Core.Extensions;
using ANCIA.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ProcessResult<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProcessResult<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return ProcessResult<string>.Fail(request.ValidationResult.ToNotificationList());

            var appUser = new AppUser(request.Email);
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, request.Password);
            if (!identityResult.Succeeded)
                return ProcessResult<string>.Fail(getErrorsFromIdentiy(identityResult));

            return ProcessResult<string>.Success(request.Email);
        }

        private IEnumerable<Notification> getErrorsFromIdentiy(IdentityResult identityResult)
        {
            return identityResult.Errors
                    .Select(error => new Notification(error.Description))
                    .ToList();
        }
    }
}
