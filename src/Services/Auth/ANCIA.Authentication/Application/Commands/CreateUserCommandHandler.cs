using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Extensions;
using ANCIA.Core.Messages.Commands;
using ANCIA.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Application.Commands
{
    public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, CommandResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, INotifier notifier) : base(notifier)
        {
            _userManager = userManager;
        }

        public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                var a = request.ValidationResult.ToNotificationList();
                _notifier.AddNotifications(a);
                return CommandResult.Empty();
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                _notifier.AddNotification("Já existe um usuário com este email cadastrado");
                return CommandResult.Empty();
            }
            var appUser = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true
            };

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, request.Password);

            if (!identityResult.Succeeded)
            {
                var errorMessages = identityResult.Errors
                    .Select(error => error.Description)
                    .ToList();
                _notifier.AddNotifications(errorMessages);
            }
            return CommandResult.Empty();
        }
    }
}
