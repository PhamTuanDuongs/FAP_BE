using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private IMapper _mapper;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {

            return Ok("Hello");
        }

        [HttpPost("Add/{course}")]

        public IActionResult Post([FromBody] CreateNewCourseDTO course)
        {
            try
            {
                string result = _courseRepository.AddNewCourse(course);
                if (result.Equals("Add a new course successfully")) return Ok(new { status = 200, message = result });
                if (result.Equals("Add a new course fail")) return Conflict(new { status = 409, message = result });
                return Conflict(new { status = 409, message = result });

            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 409, message = "Add a failed product" });
            }
        }
    }
}