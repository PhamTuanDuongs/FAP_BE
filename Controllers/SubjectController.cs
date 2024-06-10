using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet("GetAllSubjects")]
        public IActionResult GetAllSubjects()
        {
            try
            {
                List<Subject> list = _subjectRepository.GetAllSubjects();
                if(list.Count == 0 || list == null) return NotFound();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSubjectById/{sid}")]
        public IActionResult GetSubjectById(int sid)
        {
            try
            {
                var subject = _subjectRepository.GetSubjectById(sid);
                if(subject == null) return NotFound();
                return Ok(subject);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSubjectByCode/{code}")]
        public IActionResult GetSubjectByCode(string code)
        {
            try
            {
                var subject = _subjectRepository.GetSubjectByCode(code);
                if (subject == null) return NotFound();
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewSubject")]
        public IActionResult AddNewSubject(SubjectDTO subject)
        {
            try
            {
                var exist = _subjectRepository.GetSubjectByCode(subject.Code);
                if (exist != null) return Conflict("Dupplicate code subject");
                bool check = _subjectRepository.AddNewSubject(subject);
                if(!check) return Conflict("Add new subject fail");
                return Ok("Add new subject successfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSubject/{id}")]
        public IActionResult UpdateSubject(int id, SubjectDTO subject)
        {
            try
            {
                if(_subjectRepository.GetSubjectById(id) == null) return NotFound();
                if(_subjectRepository.GetSubjectByCode(subject.Code) != null) return Conflict("Dupplicate code subject");
                bool check = _subjectRepository.UpdateSubject(id,subject);
                if (!check) return Conflict();
                return Ok("Update new subject successfully");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSubject/{id}")]
        public IActionResult DeleteSubject(int id)
        {
            try
            {
                if (_subjectRepository.GetSubjectById(id) == null) return NotFound();
                bool check = _subjectRepository.DeleteSubject(id);
                if(!check) return Conflict();
                return Ok("Delete subject successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
}
}
