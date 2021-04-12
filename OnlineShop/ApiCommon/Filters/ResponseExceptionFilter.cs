using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCommon.Filters
{
    public class ResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BaseAppException exception)
            {
                var code = exception switch
                {
                    ConcurrencyException e => 409,
                    NotFoundException e => 404,
                    RestException e => e.StatusCode,
                    _ => 422,
                };
                
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = code,
                };
                
                context.ExceptionHandled = true;
            }
            else if(context.Exception != null)
            {
                context.Result = new ObjectResult("Unknown error occured while processing request")
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}