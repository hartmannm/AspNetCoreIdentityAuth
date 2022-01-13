using ANCIA.Authentication.Application.Validations;
using ANCIA.Core.Core;
using ANCIA.Core.Messages.Commands;
using FluentValidation;

namespace ANCIA.Authentication.Application.Commands
{
    public class CreateUserCommand : Command<ProcessResult<string>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Email).EmailValid();

            RuleFor(c => c.Password).PasswordValid();

            RuleFor(c => c.ConfirmPassword)
                .NotNull().WithMessage("A confirmação de senha é obrigatória");

            RuleFor(c => c.ConfirmPassword)
                .Equal(c => c.Password).WithMessage("Os campos de senha devem possuir o mesmo valor")
                .When(c => !string.IsNullOrEmpty(c.Password));
        }
    }
}
