using System;

namespace CoreModels.Messaging
{
    public sealed class CommandMeta
    {
        public string UserId { get; }
        public Guid CommandId { get; }
        public Guid CorrelationId { get; }

        
        public CommandMeta(string userId, Guid commandId, Guid correlationId)
        {
            UserId = userId;
            CommandId = commandId;
            CorrelationId = correlationId;
        }
    }
}