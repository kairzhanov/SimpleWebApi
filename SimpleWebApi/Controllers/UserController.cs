using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApi.Processors;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserProcessor _userProcessor;

        public UserController(ILogger<UserController> logger, UserProcessor userProcessor)
        {
            _logger = logger;
            _userProcessor = userProcessor;
        }
        // GET
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("/users request is received");
            var result = await _userProcessor.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            _logger.LogInformation($"/users/{userId} request is received");
            if (userId < 1)
            {
                _logger.LogInformation("/users/{userId} Bad Request");
                return BadRequest();
            } 
            
            var result = await _userProcessor.GetUser(userId);
            
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser(User user)
        {
            _logger.LogInformation($"/users/ POST request is received");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"/users/ POST bad request");
                return ValidationProblem();
            }

            var newUser = await _userProcessor.CreateUser(user);
            return Created("" , newUser);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            _logger.LogInformation($"/users/ PUT request is received");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"/users/ PUT bad request");
                return ValidationProblem();
            }

            var result = await _userProcessor.UpdateUser(user);
            if (result == -1)
                return Problem();
                
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            _logger.LogInformation($"/users/{userId} DELETE request is received");
            var result = await _userProcessor.DeleteUser(userId);

            if (result == -1)
                return Problem("User id doesn't exist");
            
            return Ok();
        }
        
    }
}