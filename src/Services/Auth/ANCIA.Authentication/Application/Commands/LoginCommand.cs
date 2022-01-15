using ANCIA.Authentication.Application.DTOs;
using ANCIA.Authentication.Application.Validations;
using ANCIA.Core.Core;
using ANCIA.Core.Messages.Commands;
using FluentValidation;

namespace ANCIA.Authentication.Application.Commands
{
    public class LoginCommand : Command<ProcessResult<LoginResultDto>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email).EmailValid();

            RuleFor(c => c.Password).PasswordValid();
        }
    }
}
