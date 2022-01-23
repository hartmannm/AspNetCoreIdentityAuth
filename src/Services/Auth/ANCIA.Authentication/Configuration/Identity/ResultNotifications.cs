using ANCIA.Core.Notifications;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Configuration.Identity
{
    public static class ResultNotifications
    {
        public static IEnumerable<Notification> ToNotifications(this IdentityResult result)
        {
            return result.Errors
                    .Select(error => new Notification(error.Description))
                    .ToList();
        }
    }
}
