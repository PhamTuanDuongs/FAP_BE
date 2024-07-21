using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;

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
        //[Authorize(Roles = "Teacher,Admin")]
        [HttpGet("GetAllInstructors")]
        public IActionResult GetAllInstructors()
        {
            try
            {
                List<Instructor> list = _instructorRepository.GetAllInstructor();
                if (list.Count == 0 || list == null) return NotFound("Not found");
                var resultMapping = _mapper.Map< List<Instructor>, List<InstructorInfoDTO>>(list);
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
                if (instructor == null) return NotFound("Not found");
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
                if (instructor == null) return NotFound("Not found");
                var resultMapping = _mapper.Map<Instructor, InstructorInfoDTO>(instructor);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInstructorImageByName/{name}")]
        public IActionResult GetInstuctorImageByName(string name)
        {
            try
            {
                string imagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images", name);
                if (System.IO.File.Exists(imagePath))
                {
                    var imageData = System.IO.File.ReadAllBytes(imagePath);
                    return File(imageData, "image/jpeg");
                }
                else
                {
                    return NotFound("Image not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewInstructor")]
        public async Task<IActionResult> AddNewInstructor([FromForm] CreateNewInstructorDTO newInstructorDTO, IFormFile file)
        {
            try
            {
                string result = _instructorRepository.AddNewInstructor(newInstructorDTO);
                if(result.Equals("Add a new instructor successfully"))
                {
                    string folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images");
                    string fileName = $"{newInstructorDTO.InstructorCode}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(folderPath, fileName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return Ok(result);
                }
                return Conflict(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateInstructor/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromForm] CreateNewInstructorDTO subject, IFormFile? file)
        {
            try
            {
                if (_instructorRepository.GetInstructorById(id) == null) return NotFound("Not found");
                bool check = _instructorRepository.UpdateInstructor(id, subject);
                if (!check) return Conflict("Update  fail");

                if (file != null)
                {
                    string folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images");
                    string fileName = $"{subject.InstructorCode}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(folderPath, fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

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
                var subject = _instructorRepository.GetInstructorById(id);
                if (subject == null) return NotFound("Not found");

                string folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images");
                string fileName = $"{subject.MetaData.Image}";
                string filePath = Path.Combine(folderPath, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

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
