using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using StaticSphere.LoggingSample.Filters;
using StaticSphere.LoggingSample.Services.Contracts;
using Xunit;

namespace StaticSphere.LoggingSample.Tests
{
    public class ActionLoggingFilterTests
    {
        private readonly Mock<ILoggerService> _loggerService;
        private readonly ActionContext _actionContext;

        public ActionLoggingFilterTests()
        {
            _loggerService = new Mock<ILoggerService>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Controller");
            routeData.Values.Add("action", "Action");
            _actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());
        }

        [Fact]
        public void OnActionExecutingLogsDebugMessage()
        {
            var actionExecutingContext = new ActionExecutingContext(
                _actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                null);
            var message = "Starting execution of GET request on Controller.Action.";
            _loggerService.Setup(x => x.LogDebug(message));
            var filter = new ActionLoggingFilter(_loggerService.Object);

            filter.OnActionExecuting(actionExecutingContext);

            _loggerService.Verify(x => x.LogDebug(message), Times.Once);
        }

        [Fact]
        public void OnActionExecutedLogsDebugMessage()
        {
            var actionExecutedContext = new ActionExecutedContext(
                _actionContext,
                new List<IFilterMetadata>(),
                null);

            var message = "Completed execution of GET request on Controller.Action.";
            _loggerService.Setup(x => x.LogDebug(message));
            var filter = new ActionLoggingFilter(_loggerService.Object);

            filter.OnActionExecuted(actionExecutedContext);

            _loggerService.Verify(x => x.LogDebug(message), Times.Once);
        }
    }
}