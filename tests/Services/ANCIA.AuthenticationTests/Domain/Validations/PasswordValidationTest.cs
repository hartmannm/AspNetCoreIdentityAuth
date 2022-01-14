using ANCIA.Authentication.Domain.Validations;
using Xunit;

namespace ANCIA.AuthenticationTests.Domain.Validations
{
    public class PasswordValidationTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("teste12")]
        public void Deve_RetornarErro_Quando_SenhaTamanhoInvalido(string password)
        {
            Assert.False(PaswordValidation.MinLength(password));
        }

        [Theory]
        [InlineData("teste123")]
        [InlineData("teste1234")]
        public void Deve_RetornarSucesso_Quando_SenhaTamanhoValido(string password)
        {
            Assert.True(PaswordValidation.MinLength(password));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Teste@1")]
        [InlineData("Teste123")]
        [InlineData("Teste@@@")]
        [InlineData("teste@123")]
        public void Deve_RetornarErro_Quando_SenhaFormatoInvalido(string password)
        {
            Assert.False(PaswordValidation.Pattern(password));
        }

        [Fact]
        public void Deve_RetornarSucesso_Quando_SenhaFormatoValido()
        {
            Assert.True(PaswordValidation.Pattern("Teste@123"));
        }
    }
}
