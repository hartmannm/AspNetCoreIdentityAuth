using ANCIA.Core.Notifications;
using FluentValidation.Results;

namespace ANCIA.Core.Extensions
{
    public static class ValidationResultExtensions
    {
        public static IEnumerable<Notification> ToNotificationList(this ValidationResult validationResult)
        {
            return validationResult.Errors
                    .Select(error => new Notification(error.ErrorMessage))
                    .ToList();
        }
    }
}
