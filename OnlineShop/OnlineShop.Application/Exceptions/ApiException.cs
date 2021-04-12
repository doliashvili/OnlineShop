using System;
using System.Globalization;

namespace OnlineShop.Application.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        
        public ApiException() : base()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(int statusCode) : base()
        {
            StatusCode = statusCode;
        }
        
        public ApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
        
        public ApiException(int statusCode, string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            StatusCode = statusCode;
        }

        public static ApiException NotFound(Type resource, object id)
            => new ApiException(404, $"Resource:{resource.Name} with id:{id} not found");
    }
}