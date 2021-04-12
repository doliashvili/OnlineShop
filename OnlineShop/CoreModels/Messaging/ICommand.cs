namespace CoreModels.Messaging
{
    public interface ICommand : IMessage
    {
        CommandMeta CommandMeta { get; }
    }
}