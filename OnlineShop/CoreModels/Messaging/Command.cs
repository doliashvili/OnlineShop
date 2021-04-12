namespace CoreModels.Messaging
{
    public abstract class Command : ICommand
    {
        public CommandMeta CommandMeta { get; }

        protected Command(CommandMeta commandMeta)
        {
            CommandMeta = commandMeta;
        }
    }
}