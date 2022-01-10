using ANCIA.Core.Messages.Commands;
using FluentValidation;

namespace ANCIA.Authentication.Application.Commands
{
    public class CreateUserCommand : Command
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
            RuleFor(c => c.Email)
                .NotNull().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("Endereço de email em formato inválido");

            RuleFor(c => c.Password)
                .NotNull().WithMessage("A senha é obrigatória")
                .MinimumLength(8).WithMessage("A senha deve conter no mínimo {MinLength} caracteres")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Senha em formato inválido");

            RuleFor(c => c.ConfirmPassword)
                .NotNull().WithMessage("A confirmação de senha é obrigatória");

            RuleFor(c => c.ConfirmPassword)
                .Equal(c => c.Password).WithMessage("Os campos de senha devem possuir o mesmo valor")
                .When(c => !string.IsNullOrEmpty(c.Password));
        }
    }
}
