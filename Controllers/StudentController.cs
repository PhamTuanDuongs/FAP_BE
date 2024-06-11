using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            try
            {
                List<Student> list = _studentRepository.GetAllStudents();
                if (list.Count == 0 || list == null) return NotFound();
                var resultMapping = _mapper.Map<List<StudentInfoDTO>>(list);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetStudentByRolenumber/{rolenumber}")]
        public IActionResult GetStudentByRolenumber(string rolenumber)
        {
            try
            {
                var student = _studentRepository.GetStudentByRoleNumber(rolenumber);
                if (student == null) return NotFound();
                var resultMapping = _mapper.Map<Student, StudentInfoDTO>(student);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentById/{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _studentRepository.GetStudentById(id);
                if (student == null) return NotFound();
                var resultMapping = _mapper.Map<Student, StudentInfoDTO>(student);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewStudent")]
        public IActionResult AddNewStudent(CreateNewStudentDTO newStudentDTO)
        {
            try
            {
                string result = _studentRepository.AddNewStudent(newStudentDTO);
                if (result.Equals("Add a new student successfully")) return Ok(result);
                return Conflict(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateStudent")]
        public IActionResult UpdateStudent(int id, CreateNewStudentDTO subject)
        {
            try
            {
                if (_studentRepository.GetStudentById(id) == null) return NotFound();
                bool check = _studentRepository.UpdateStudent(id, subject);
                if (!check) return Conflict("Update student fail");
                return Ok("Update student successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                if(_studentRepository.GetStudentById(id) == null) return NotFound();
                bool check = _studentRepository.DeleteStudent(id);
                if(!check) return Conflict("Delete student fail");
                return Ok("Delete student successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
