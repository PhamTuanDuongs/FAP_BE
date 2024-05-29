using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult Get()
        {

            return Ok("Hello");
        }
    }
}
