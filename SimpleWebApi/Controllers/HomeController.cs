using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApi.Processors;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        // GET
        private readonly ILogger<HomeController> _logger;
        private readonly ProductProcessor _productProcessor;

        public HomeController(ILogger<HomeController> logger, ProductProcessor productProcessor)
        {
            _logger = logger;
            _productProcessor = productProcessor;
        }
         
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("/ request is received");
            return Ok(new Dictionary<string, string> {
                {"status", "OK"}
            } );
        }
    }
}