namespace ANCIA.Core.Messages.Commands
{
    public class CommandResult
    {
        public object Value { get; private set; }

        public CommandResult(object value)
        {
            Value = value;
        }

        public static CommandResult Empty()
        {
            return new CommandResult(null);
        }
    }
}
