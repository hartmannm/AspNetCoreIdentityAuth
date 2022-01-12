using ANCIA.Core.Core;
using ANCIA.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ANCIA.CoreTests.Core
{
    public class ProcessResultTest
    {
        [Fact]
        public void Deve_CriarResultadoSemErros_Quando_UtilizadoConstrutorDeResultado()
        {
            var result = new ProcessResult<string>("teste");
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Deve_CriarResultadoComErrosSemConteudo_Quando_UtilizadoConstrutorErros()
        {
            var notifications = new List<Notification>();
            notifications.Add(new Notification("teste1"));
            notifications.Add(new Notification("teste2"));
            var result = new ProcessResult<string>(notifications);
            Assert.Null(result.Content);
            Assert.Collection(result.Errors,
                    item => Assert.Equal("teste1", item.Message),
                    item => Assert.Equal("teste2", item.Message)
                );
        }

        [Fact]
        public void Deve_CriarResultadoComUmErroSemConteudo_Quando_UtilizadoConstrutorErro()
        {
            var result = new ProcessResult<string>(new Notification("teste1"));
            Assert.Null(result.Content);
            Assert.Collection(result.Errors,
                    item => Assert.Equal("teste1", item.Message)
                );
        }

        [Fact]
        public void Deve_CriarResultadoSemErros_Quando_UtilizadoMetodoEstaticoSucesso()
        {
            var result = ProcessResult<string>.Success("teste");
            Assert.Empty(result.Errors);
            Assert.Equal("teste", result.Content);
        }

        [Fact]
        public void Deve_CriarResultadoComErrosSemConteudo_Quando_UtilizadoMetodoEstaticoNotification()
        {
            var notification = new Notification("teste1");
            var result = ProcessResult<string>.Fail(notification);
            Assert.Null(result.Content);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Deve_CriarResultadoComErrosSemConteudo_Quando_UtilizadoMetodoEstaticoListaNotifications()
        {
            var notifications = new List<Notification>();
            notifications.Add(new Notification("teste1"));
            notifications.Add(new Notification("teste2"));
            var result = ProcessResult<string>.Fail(notifications);
            Assert.Null(result.Content);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Deve_CriarResultadoComErrosSemConteudo_Quando_UtilizadoMetodoEstaticoString()
        {
            var result = ProcessResult<string>.Fail("teste1");
            Assert.Null(result.Content);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Deve_CriarResultadoComErrosSemConteudo_Quando_UtilizadoMetodoEstaticoException()
        {
            var result = ProcessResult<string>.Fail(new Exception("teste"));
            Assert.Null(result.Content);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Deve_RetornarQueNaoExisteErros_Quando_ResultSucesso()
        {
            var result = ProcessResult<string>.Success("teste");
            Assert.False(result.HasError());
        }

        [Fact]
        public void Deve_RetornarQueExisteErros_Quando_ResultErro()
        {
            var result = ProcessResult<string>.Fail("teste");
            Assert.True(result.HasError());
        }
    }
}
