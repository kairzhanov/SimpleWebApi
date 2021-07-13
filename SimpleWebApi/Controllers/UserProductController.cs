using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApi.Models;
using SimpleWebApi.Processors;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("user-products")]
    public class UserProductController : Controller
    {
        private readonly ILogger<UserProductController> _logger;
        private readonly ProductProcessor _productProcessor;
        private readonly UserProductProcessor _userProductProcessor;
        
        public UserProductController(ILogger<UserProductController> logger, 
            ProductProcessor productProcessor,
            UserProductProcessor userProductProcessor)
        {
            _logger = logger;
            _productProcessor = productProcessor;
            _userProductProcessor = userProductProcessor;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogTrace("/user-products request is received");
            var list = await _userProductProcessor.GetAllUserProductsAsync();
            
            return Ok(list);
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserProducts(int userId)
        {
            _logger.LogTrace($"/user-products/user/{userId} request is received");
            var userProducts = await _userProductProcessor.GetUserProductsAsync(userId);
            
            return Ok(userProducts);
        }
    }
}