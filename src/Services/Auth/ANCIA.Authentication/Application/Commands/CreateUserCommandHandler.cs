using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Core;
using ANCIA.Core.Extensions;
using ANCIA.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace ANCIA.Authentication.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ProcessResult<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ProcessResult<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return ProcessResult<string>.Fail(request.ValidationResult.ToNotificationList());

            var appUser = new AppUser(request.Email);

            return await CreateUser(request, appUser);
        }

        private async Task<ProcessResult<string>> CreateUser(CreateUserCommand request, AppUser appUser)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId);
                if (role is null)
                    return ProcessResult<string>.Fail("Role não encontrada");

                IdentityResult createUserResult = await _userManager.CreateAsync(appUser, request.Password);
                if (!createUserResult.Succeeded)
                    return ProcessResult<string>.Fail(getErrorsFromIdentiy(createUserResult));

                var addRoleresult = await _userManager.AddToRoleAsync(appUser, role.Name);
                if (!addRoleresult.Succeeded)
                {
                    scope.Dispose();
                    return ProcessResult<string>.Fail(getErrorsFromIdentiy(addRoleresult));
                }

                scope.Complete();
                return ProcessResult<string>.Success(request.Email);
            }
        }

        private IEnumerable<Notification> getErrorsFromIdentiy(IdentityResult identityResult)
        {
            return identityResult.Errors
                    .Select(error => new Notification(error.Description))
                    .ToList();
        }
    }
}
