using System.Text.RegularExpressions;

namespace ANCIA.Authentication.Domain.Validations
{
    public class PaswordValidation
    {
        public static readonly string PasswordRequiredMessage = "A senha é obrigatória";
        public static readonly string PasswordLengthMessage = "A senha deve conter no mínimo 8 caracteres";
        public static readonly string PasswordPatternMessage = "Senha em formato inválido";

        private static readonly Regex _passwordPattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        public static bool MinLength(string password)
        {
            return password.Length >= 8;
        }

        public static bool Pattern(string password)
        {
            return _passwordPattern.Match(password).Success;
        }
    }
}
