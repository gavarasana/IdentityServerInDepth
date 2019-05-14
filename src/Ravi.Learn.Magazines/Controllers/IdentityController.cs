using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ravi.Learn.Magazines.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public IdentityController(ILogger<IdentityController> logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        [HttpGet]
        public IActionResult Get()
        {
            Logger.LogDebug("Retrieving claims from Identity");
            return new JsonResult(User.Claims.Select(c => new { c.Type, c.Value }));
        }
    }
}