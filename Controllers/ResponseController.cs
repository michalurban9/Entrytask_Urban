namespace Entrytask_Urban.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace Entrytask_Urban.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class ResponseController : ControllerBase
        {
            [HttpPost("processResponse")]
            public IActionResult ProcessResponse([FromForm] string response)
            {
                // Process the response in your C# code
                Console.WriteLine($"Received response: {response}");

                // Return an HTTP response
                return Ok();
            }
        }
    }
}
