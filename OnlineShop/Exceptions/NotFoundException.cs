using System;

namespace Exceptions
{
    public class NotFoundException : BaseAppException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(Type resource, object id) 
            : base($"Resource {resource?.Name} with id: {id} not found.")
        {
        }
    }
}