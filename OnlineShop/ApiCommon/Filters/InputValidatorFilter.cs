using System.Linq;
using Helpers.ReadResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCommon.Filters
{
    public class InputValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var resultModel = Result<object[]>.Fail("The submitted data does not meet the server requirements.");
                
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new
                    {
                        Name = e.Key,
                        Message = e.Value.Errors.First().ErrorMessage
                    }).ToArray();

                resultModel.Data = errors;
                actionContext.Result = new BadRequestObjectResult(resultModel);
            }

        }
    }
}
