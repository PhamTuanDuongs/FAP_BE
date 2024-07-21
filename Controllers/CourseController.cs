using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
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
                return BadRequest(new { status = 409, message = "Add a failed course" });
            }
        }

        [HttpGet("GetCourses")]
        public IActionResult GetCourses()
        {
            try
            {
                var list = _courseRepository.GetCourses();
                var listCourse = _mapper.Map<List<ListCourseDTO>>(list);
                return Ok(listCourse);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCourseInstructorId/{instructorId}")]
        public IActionResult GetCourseByInstructorId(int instructorId)
        {
            try
            {
                var course = _mapper.Map <List<ListCourseDTO>>(_courseRepository.GetCourseByInstructorId(instructorId));
                return Ok(course);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}