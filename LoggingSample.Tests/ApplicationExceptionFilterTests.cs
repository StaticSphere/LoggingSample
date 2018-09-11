using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Routing;
using Moq;
using StaticSphere.LoggingSample.Filters;
using StaticSphere.LoggingSample.Services.Contracts;
using System;
using System.Collections.Generic;
using Xunit;

namespace StaticSphere.LoggingSample.Tests
{
    public class ApplicationExceptionFilterTests
    {
        private readonly Mock<IHostingService> _hostingService;
        private readonly Mock<IModelMetadataProvider> _modelMetadataProvider;
        private readonly Mock<ILoggerService> _loggerService;
        private readonly ActionContext _actionContext;

        public ApplicationExceptionFilterTests()
        {
            _hostingService = new Mock<IHostingService>();
            _modelMetadataProvider = new Mock<IModelMetadataProvider>();
            _loggerService = new Mock<ILoggerService>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Controller");
            routeData.Values.Add("action", "Action");
            _actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());
        }

        [Fact]
        public void OnExceptionLogsErrorMessage()
        {
            var exception = new ApplicationException("Test");
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = exception
            };
            var message = "An exception was encountered during the execution of a GET request on Controller.Action.";
            _loggerService.Setup(x => x.LogError(exception, message));
            var filter = new ApplicationExceptionFilter(_hostingService.Object, _modelMetadataProvider.Object, _loggerService.Object);

            filter.OnException(exceptionContext);

            _loggerService.Verify(x => x.LogError(exception, message), Times.Once);
        }

        [Fact]
        public void OnExceptionReturnsViewResultWhenInDevelopmentMode()
        {
            _hostingService.Setup(x => x.IsDevelopment()).Returns(true);
            var identity = ModelMetadataIdentity.ForType(typeof(object));
            var metadata = new Mock<ModelMetadata>(identity) { CallBase = true };
            _modelMetadataProvider.Setup(x => x.GetMetadataForType(typeof(object))).Returns(metadata.Object);

            var exception = new ApplicationException("Test");
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = exception
            };
            var filter = new ApplicationExceptionFilter(_hostingService.Object, _modelMetadataProvider.Object, _loggerService.Object);

            filter.OnException(exceptionContext);

            var result = exceptionContext.Result as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("CustomError", result.ViewName);
            Assert.NotNull(result.ViewData);
            Assert.True(result.ViewData.Keys.Contains("Exception"));
            Assert.Equal(exception, result.ViewData["Exception"]);
        }

        [Fact]
        public void OnExceptionDoesNotModifyResultWhenNotInDevelopmentMode()
        {
            _hostingService.Setup(x => x.IsDevelopment()).Returns(false);
            var exception = new ApplicationException("Test");
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = exception
            };
            var filter = new ApplicationExceptionFilter(_hostingService.Object, _modelMetadataProvider.Object, _loggerService.Object);

            filter.OnException(exceptionContext);

            var result = exceptionContext.Result;

            Assert.Null(result);
        }
    }
}
