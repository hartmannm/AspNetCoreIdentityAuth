using ANCIA.Core.Notifications;

namespace ANCIA.Core.Core
{
    public class ProcessResult<T>
    {
        private ICollection<Notification> _errors;

        public T? Content { get; private set; }

        public IReadOnlyCollection<Notification> Errors
        {
            get => _errors.ToList().AsReadOnly();
        }

        public ProcessResult(T content)
        {
            _errors = new List<Notification>();
            Content = content;
        }

        public ProcessResult(ICollection<Notification> errors)
        {
            _errors = errors;
        }

        public ProcessResult(Notification error)
        {
            _errors = new List<Notification>();
            _errors.Add(error);
        }

        public static ProcessResult<T> Success(T content)
        {
            return new ProcessResult<T>(content);
        }

        public static ProcessResult<T> Fail(Notification notification)
        {
            return new ProcessResult<T>(notification);
        }

        public static ProcessResult<T> Fail(IEnumerable<Notification> notification)
        {
            return new ProcessResult<T>(notification.ToList());
        }

        public static ProcessResult<T> Fail(string errorMessage)
        {
            return new ProcessResult<T>(new Notification(errorMessage));
        }

        public static ProcessResult<T> Fail(Exception error)
        {
            return new ProcessResult<T>(new Notification(error.Message));
        }

        public bool HasError() => _errors.Any();
    }
}
