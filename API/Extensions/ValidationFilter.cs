using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedSources.Extensions;
using SharedSources.Models;

namespace API.Extensions;

/// <summary>
/// <see cref="ValidationFilter"/> class
/// </summary>
public class ValidationFilter : IAsyncActionFilter
{
    /// <summary>
    /// <see cref="OnActionExecutionAsync(ActionExecutingContext, ActionExecutionDelegate)"/> method
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            Dictionary<string, IEnumerable<string>> errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage));

            var errorResponse = errorsInModelState.Select(error => new 
            {
                FieldName = error.Key,
                Message = error.Value,
            }).ToList();

            StandarResponseModel<object>?response = new StandarResponseModel<object>().SetError(errorResponse);
            context.Result = new BadRequestObjectResult(response);
            
            return;
        }

        await next();
    }
}
