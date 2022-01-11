using FluentValidation.Results;
using MediatR;

namespace ANCIA.Core.Messages.Commands
{
    public abstract class Command<T> : IRequest<T>
    {
        public DateTime CreatedAt { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        public Command()
        {
            CreatedAt = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid() => ValidationResult.IsValid;
    }
}
