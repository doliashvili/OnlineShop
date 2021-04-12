using System;

namespace Exceptions
{
    public class ConcurrencyException : BaseAppException
    {
        public ConcurrencyException()
        {
        }

        public ConcurrencyException(string message) : base(message)
        {
        }

        public ConcurrencyException(Type resource) : base($"optimistic concurrency conflict on resource: {resource.Name}.")
        {
        }
    }
}