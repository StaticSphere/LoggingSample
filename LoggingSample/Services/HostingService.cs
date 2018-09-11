using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using StaticSphere.LoggingSample.Services.Contracts;

namespace StaticSphere.LoggingSample.Services
{
    [ExcludeFromCodeCoverage]
    public class HostingService : IHostingService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HostingService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public bool IsDevelopment() => _hostingEnvironment.IsDevelopment();
    }
}