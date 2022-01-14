using ANCIA.Authentication.Domain.Validations;
using FluentValidation;

namespace ANCIA.Authentication.Application.Validations
{
    public static class PasswordValidator
    {
        public static IRuleBuilderOptions<T, string?> PasswordValid<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            ruleBuilder.NotNull()
               .NotEmpty()
               .WithMessage(PaswordValidation.PasswordRequiredMessage);

            ruleBuilder.Must(password => PaswordValidation.MinLength(password) == true)
                .WithMessage(PaswordValidation.PasswordLengthMessage);

            return ruleBuilder.Must(password => PaswordValidation.Pattern(password) == true)
                .WithMessage(PaswordValidation.PasswordPatternMessage);
        }
    }
}
