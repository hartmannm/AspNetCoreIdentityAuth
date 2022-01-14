using ANCIA.Authentication.Domain.Validations;
using FluentValidation;

namespace ANCIA.Authentication.Application.Validations
{
    public static class EmailValidator
    {
        public static IRuleBuilderOptions<T, string?> EmailValid<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            ruleBuilder.NotNull()
                .NotEmpty()
                .WithMessage(EmailvalidationMessages.EmailRequired);

            return ruleBuilder.EmailAddress()
                .WithMessage(EmailvalidationMessages.EmailInvalidFormat);
        }
    }
}
