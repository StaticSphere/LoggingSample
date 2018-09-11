using Microsoft.AspNetCore.Mvc.Filters;
using StaticSphere.LoggingSample.Services.Contracts;

namespace StaticSphere.LoggingSample.Filters
{
    public class ActionLoggingFilter : IActionFilter
{
    private readonly ILoggerService _logger;
 
    public ActionLoggingFilter(ILoggerService loggerService)
    {
        _logger = loggerService;
    }
 
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var method = context.HttpContext.Request.Method;
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        var message = $"Starting execution of {method} request on {controller}.{action}.";
 
        _logger.LogDebug(message);
    }
 
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var method = context.HttpContext.Request.Method;
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        var message = $"Completed execution of {method} request on {controller}.{action}.";
 
        _logger.LogDebug(message);
    }
}
}