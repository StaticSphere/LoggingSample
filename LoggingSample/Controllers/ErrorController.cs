using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StaticSphere.LoggingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            throw new ApplicationException("Oops");
        }
    }
}
