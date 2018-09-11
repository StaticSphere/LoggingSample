using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using StaticSphere.LoggingSample.Services.Contracts;

namespace StaticSphere.LoggingSample.Filters
{
    public class ApplicationExceptionFilter : IExceptionFilter
    {
        private readonly IHostingService _hostingService;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILoggerService _logger;

        public ApplicationExceptionFilter(IHostingService hostingService, IModelMetadataProvider modelMetadataProvider, ILoggerService loggerService)
        {
            _hostingService = hostingService;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = loggerService;
        }

        public void OnException(ExceptionContext context)
        {
            var method = context.HttpContext.Request.Method;
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];
            var message = $"An exception was encountered during the execution of a {method} request on {controller}.{action}.";

            _logger.LogError(context.Exception, message);

            if (!_hostingService.IsDevelopment())
                return;

            var result = new ViewResult { ViewName = "CustomError" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            {
                { "Exception", context.Exception }
            };
            context.Result = result;
        }
    }
}