using ANCIA.Core.Core;
using ANCIA.Core.Messages.Commands;
using FluentValidation;

namespace ANCIA.Authentication.Application.Commands
{
    public class ActivateUserCommand : Command<ProcessResult<string>>
    {
        public string UserId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ActivateUserCommandCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    internal class ActivateUserCommandCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("A identificação do usuário é obrigatória");
        }
    }
}
