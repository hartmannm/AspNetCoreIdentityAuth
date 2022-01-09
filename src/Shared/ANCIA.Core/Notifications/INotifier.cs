namespace ANCIA.Core.Notifications
{
    public interface INotifier
    {
        public bool HasNotifications();

        public void AddNotification(Notification notification);

        public void AddNotification(String notificationMessage);

        public void AddNotifications(IEnumerable<string> messages);

        public void AddNotifications(IEnumerable<Notification> notifications);

        public IEnumerable<Notification> GetNotifications();

        public IEnumerable<string> GetNotificationsMessages();
    }
}
