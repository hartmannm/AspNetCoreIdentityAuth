using ANCIA.Core.Notifications;

namespace ANCIA.Core.Messages.Commands
{
    public abstract class CommandHandler
    {
        protected INotifier _notifier;

        protected CommandHandler(INotifier notifier)
        {
            _notifier = notifier;
        }
    }
}
