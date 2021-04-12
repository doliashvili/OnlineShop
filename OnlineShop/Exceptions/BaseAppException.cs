using System;

namespace Exceptions
{
    public abstract class BaseAppException : Exception
    {
        protected BaseAppException(){ }
        protected BaseAppException(string message) : base(message)
        {
        }
    }
}