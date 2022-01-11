using ANCIA.Core.Notifications;
using System;
using System.Collections.Generic;
using Xunit;

namespace ANCIA.CoreTests.Notifications
{
    public class NotifierTest : IDisposable
    {
        private Notifier _notifier;

        public NotifierTest()
        {
            _notifier = new Notifier();
        }

        public void Dispose()
        {
            _notifier = null;
        }

        [Fact]
        public void Deve_AdicionarNotificacao_Quando_InformadaEntidadeNotificacao()
        {
            _notifier.AddNotification(new Notification("teste"));
            Assert.Single(_notifier.GetNotifications());
            Assert.Contains(_notifier.GetNotifications(), n => n.Message == "teste");
        }

        [Fact]
        public void Deve_AdicionarNotificacao_Quando_InformadaMensagemString()
        {
            _notifier.AddNotification("teste");
            Assert.Single(_notifier.GetNotifications());
            Assert.Contains(_notifier.GetNotifications(), n => n.Message == "teste");
        }

        [Fact]
        public void Deve_AdicionarListaDeNotificacoes_Quando_InformadaListaDeMensagens()
        {
            var messages = new List<string> { "teste1", "teste2" };
            _notifier.AddNotifications(messages);
            Assert.Collection(_notifier.GetNotifications(),
                    item => Assert.Equal("teste1", item.Message),
                    item => Assert.Equal("teste2", item.Message)
                );
        }

        [Fact]
        public void Deve_LancarException_Quando_AdicionarListaMensagensVazias()
        {
            Assert.Throws<ArgumentException>(() => _notifier.AddNotifications(new List<string>()));
        }

        [Fact]
        public void Deve_AdicionarListaDeNotificacoes_Quando_InformadaListaDeNotificacoes()
        {
            var messages = new List<Notification> {
                new Notification("teste1"),
                new Notification("teste2")
            };
            _notifier.AddNotifications(messages);
            Assert.Collection(_notifier.GetNotifications(),
                    item => Assert.Equal("teste1", item.Message),
                    item => Assert.Equal("teste2", item.Message)
                );
        }

        [Fact]
        public void Deve_LancarException_Quando_AdicionarListaNotificacoesVazias()
        {
            Assert.Throws<ArgumentException>(() => _notifier.AddNotifications(new List<Notification>()));
        }

        [Fact]
        public void Deve_RetornarFalso_Quando_NaoPossuirNotificacoes()
        {
            Assert.False(_notifier.HasNotifications());
        }

        [Fact]
        public void Deve_RetornarTrue_Quando_NaoPossuirNotificacoes()
        {
            _notifier.AddNotification("teste");
            Assert.True(_notifier.HasNotifications());
        }

        [Fact]
        public void Deve_RetornarListaComMensagensDeErro()
        {
            _notifier.AddNotification("teste");
            Assert.Collection(_notifier.GetNotificationsMessages(),
                    item => Assert.Equal("teste", item)
                );
        }
    }
}
