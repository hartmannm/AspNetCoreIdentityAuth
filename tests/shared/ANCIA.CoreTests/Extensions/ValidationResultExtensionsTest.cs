using ANCIA.Core.Extensions;
using ANCIA.Core.Notifications;
using FluentValidation.Results;
using Xunit;

namespace ANCIA.CoreTests.Extensions
{
    public class ValidationResultExtensionsTest
    {
        [Fact]
        public void Deve_ConverterErrosParaNotifications()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure(string.Empty, "teste1"));
            validationResult.Errors.Add(new ValidationFailure(string.Empty, "teste2"));
            var notifications = validationResult.ToNotificationList();
            Assert.Collection(notifications,
                    item => Assert.Equal("teste1", item.Message),
                    item => Assert.Equal("teste2", item.Message)
                );
            Assert.Collection(notifications,
                    item => Assert.True(item is Notification),
                    item => Assert.True(item is Notification)
                );
        }
    }
}
