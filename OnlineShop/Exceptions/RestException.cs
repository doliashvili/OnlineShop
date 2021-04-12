namespace Exceptions
{
    public class RestException : BaseAppException
    {
        public int StatusCode { get; protected set; }
        
        public RestException()
        {
        }

        public RestException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public RestException(string message) : base(message)
        {
        }
        
        public RestException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}