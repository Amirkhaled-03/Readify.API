using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Readify.API.HandleResponses;

namespace Readify.API.Filters
{
    public class ValidateModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();


                // Return custom BadRequest response with errors
                context.Result = new BadRequestObjectResult(new ApiResponse<object>(400, "Validation Errors", errors: errors));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
