using ANCIA.Authentication.Application.Commands;
using Xunit;

namespace ANCIA.AuthenticationTests.Application.Commands
{
    public class CreateUserCommandTest
    {
        [Fact]
        public void Deve_RetornarValido_Quando_EmailForValido()
        {
            var command = new CreateUserCommand
            {
                Email = "teste@teste.com",
                Password = "Teste@123",
                ConfirmPassword = "Teste@123"
            };
            Assert.True(command.IsValid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("testeteste.com")]
        public void Deve_RetornarErro_Quando_EmailFormatoInvalido(string email)
        {
            var command = new CreateUserCommand
            {
                Email = email,
                Password = "Teste@123",
                ConfirmPassword = "Teste@123"
            };
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Teste@1")]
        [InlineData("Teste123")]
        [InlineData("Teste@@@")]
        [InlineData("teste@123")]
        public void Deve_RetornarErro_Quando_SenhaInvalida(string senha)
        {
            var command = new CreateUserCommand
            {
                Email = "teste@teste.com",
                Password = senha,
                ConfirmPassword = senha
            };
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData("Teste@123", null)]
        [InlineData("Teste@123", "Teste@321")]
        public void Deve_RetornarErro_Quando_SenhasDiferentes(string password, string confirmPassword)
        {
            var command = new CreateUserCommand
            {
                Email = "teste@teste.com",
                Password = password,
                ConfirmPassword = confirmPassword
            };
            Assert.False(command.IsValid());
        }

        [Fact]
        public void Deve_RetornarValido_Quando_SenhasIguais()
        {
            var command = new CreateUserCommand
            {
                Email = "teste@teste.com",
                Password = "Teste@123",
                ConfirmPassword = "Teste@123"
            };
            Assert.True(command.IsValid());
        }
    }
}
