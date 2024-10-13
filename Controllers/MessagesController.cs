using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from .NET Core API!");
        }

        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            return Ok($"Message received: {message}");
        }
    }
}
