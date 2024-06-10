using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepository;
        private IMapper _mapper;

        public InstructorController(IInstructorRepository instructorRepository, IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllInstructors")]
        public IActionResult GetAllInstructors()
        {
            try
            {
                List<Instructor> list = _instructorRepository.GetAllInstructor();
                if (list.Count == 0 || list == null) return NotFound();
                var resultMapping = _mapper.Map<List<InstructorInfoDTO>>(list);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInstructorById/{id}")]
        public IActionResult GetInstructorById(int id)
        {
            try
            {
                var instructor = _instructorRepository.GetInstructorById(id);
                if (instructor == null) return NotFound();
                var resultMapping = _mapper.Map<Instructor, InstructorInfoDTO>(instructor);
                return Ok(resultMapping);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInstructorByCode/{code}")]
        public IActionResult GetInstructorByCode(string code)
        {
            try
            {
                var instructor = _instructorRepository.GetInstructorByCode(code);
                if (instructor == null) return NotFound();
                var resultMapping = _mapper.Map<Instructor, InstructorInfoDTO>(instructor);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewInstructor")]
        public IActionResult AddNewInstructor(CreateNewInstructorDTO newInstructorDTO)
        {
            try
            {
                string result = _instructorRepository.AddNewInstructor(newInstructorDTO);
                if(result.Equals("Add a new instructor successfully")) return Ok(result);
                return Conflict(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateInstructor")]
        public IActionResult UpdateStudent(int id, CreateNewInstructorDTO subject)
        {
            try
            {
                if (_instructorRepository.GetInstructorById(id) == null) return NotFound();
                if (_instructorRepository.GetInstructorByCode(subject.InstructorCode) != null) return Conflict("Dupplicate instructor code instructor");
                bool check = _instructorRepository.UpdateInstructor(id, subject);
                if (!check) return Conflict();
                return Ok("Update Instructor successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteInstructor/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                if (_instructorRepository.GetInstructorById(id) == null) return NotFound();
                bool check = _instructorRepository.DeleteInstuctor(id);
                if (!check) return Conflict("Delete Instructor Fail");
                return Ok("Delete Instructor Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
