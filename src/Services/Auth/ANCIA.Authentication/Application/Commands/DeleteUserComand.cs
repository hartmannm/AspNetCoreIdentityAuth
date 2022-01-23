using ANCIA.Core.Core;
using ANCIA.Core.Messages.Commands;
using FluentValidation;

namespace ANCIA.Authentication.Application.Commands
{
    public class DeleteUserComand : Command<ProcessResult<string>>
    {
        public string UserId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserComand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("A identificação do usuário é obrigatória");
        }
    }
}
