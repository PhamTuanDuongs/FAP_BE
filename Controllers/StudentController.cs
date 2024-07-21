using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Reflection;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper, IHostEnvironment env)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            try
            {
                List<Student> list = _studentRepository.GetAllStudents();
                if (list.Count == 0 || list == null) return NotFound("Not found");
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
                if (student == null) return NotFound("Not found");
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
                if (student == null) return NotFound("Not found");
                var resultMapping = _mapper.Map<Student, StudentInfoDTO>(student);
                return Ok(resultMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewStudent")]
        public async Task<IActionResult> AddNewStudent([FromForm] CreateNewStudentDTO newStudentDTO, IFormFile file)
        {
            try
            {
                string result = _studentRepository.AddNewStudent(newStudentDTO);

                if (result.Equals("Add a new student successfully"))
                {
                    string folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images");
                    string fileName = $"{newStudentDTO.RoleNumber}{Path.GetExtension(file.FileName)}";
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

        [HttpGet("GetStudentImageByName/{name}")]
        public IActionResult GetStudentImageByName(string name)
        {
            try
            {
                string imagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images",name);
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

        [HttpPut("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromForm] CreateNewStudentDTO subject, IFormFile? file)
        {
            try
            {
                if (_studentRepository.GetStudentById(id) == null) return NotFound("Not found");
                bool check = _studentRepository.UpdateStudent(id, subject);
                if (!check) return Conflict("Update student fail");

                if (file  != null)
                {
                    string folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images");
                    string fileName = $"{subject.RoleNumber}{Path.GetExtension(file.FileName)}";
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
                var subject = _studentRepository.GetStudentById(id);
                if(subject == null) return NotFound("Not found");

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

                bool check = _studentRepository.DeleteStudent(id);
                if (!check) return Conflict("Delete student fail");
                return Ok("Delete student successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExportStudentToExcel")]
        public IActionResult ExportStudentToExcel()
        {
            try
            {
                List<Student> students = _studentRepository.GetAllStudents();
                List<StudentInfoDTO> studentDtos = _mapper.Map<List<StudentInfoDTO>>(students);

                MemoryStream stream = new MemoryStream();
                _studentRepository.ExportStudentToExcel(studentDtos, stream);
                stream.Position = 0;

                return new FileStreamResult(stream, "text/csv")
                {
                    FileDownloadName = "StudentsInfo.csv"
                };
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
