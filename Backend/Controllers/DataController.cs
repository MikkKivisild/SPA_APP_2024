using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            // Your protected data retrieval logic here
            return Ok(new { message = "Protected data retrieved successfully!" });
        }
    }
}
