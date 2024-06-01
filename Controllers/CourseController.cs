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

        [HttpPost("Add/{courseDTO}")]

        public IActionResult Post([FromBody] CreateNewCourseDTO courseDTO)
        {
//            delete from Attendance
//delete from Schedule
//delete from StudentCourse
//delete from Course
            try
            {
                if (_courseRepository.AddNewCourse(courseDTO)) return Ok();
                return BadRequest("In Try");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
