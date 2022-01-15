using ANCIA.Authentication.Application.AuthToken;
using ANCIA.Authentication.Application.DTOs;
using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Core;
using ANCIA.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Application.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ProcessResult<LoginResultDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenManager _tokenManager;

        public LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
        }

        public async Task<ProcessResult<LoginResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return ProcessResult<LoginResultDto>.Fail(request.ValidationResult.ToNotificationList());

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: true);
            if (!IsLoginValid(result))
                return ProcessResult<LoginResultDto>.Fail(GetLoginErrorMessage(result));

            var user = await _userManager.FindByEmailAsync(request.Email);
            var token = await _tokenManager.CreateToken(user);
            var loginResult = new LoginResultDto(token, user.Id, user.Email);
            return ProcessResult<LoginResultDto>.Success(loginResult);
        }

        private bool IsLoginValid(SignInResult result) => !result.IsLockedOut && result.Succeeded;

        private string GetLoginErrorMessage(SignInResult result)
        {
            if (result.IsLockedOut)
                return "Quantidade de tentativas de login excedida, tente novamente mais tarde";
            else
                return "Usuário ou senha inválidos";
        }

    }
}
