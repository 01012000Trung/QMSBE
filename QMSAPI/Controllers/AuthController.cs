using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API is working!");
        }
    } // Ensure this closing brace is present to properly close the class definition
}
