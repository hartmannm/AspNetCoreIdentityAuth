namespace ANCIA.Core.Notifications
{
    public class Notifier : INotifier
    {
        private ICollection<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotification(string notificationMessage)
        {
            _notifications.Add(new Notification(notificationMessage));
        }

        public void AddNotifications(IEnumerable<string> messages)
        {
            messages.Select(msg => new Notification(msg))
                .ToList()
                .ForEach(notification => _notifications.Add(notification));
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            notifications.ToList()
                .ForEach(notification => _notifications.Add(notification));
        }

        public IEnumerable<Notification> GetNotifications() => _notifications.ToList().AsReadOnly();

        public IEnumerable<string> GetNotificationsMessages()
        {
            return _notifications.Select(notification => notification.Message)
                .ToList();
        }

        public bool HasNotifications() => _notifications.Any();
    }
}
