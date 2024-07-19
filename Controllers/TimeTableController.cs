using AutoMapper;
using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableController : ControllerBase
    {
        private readonly ITimetableRepository _timetableRepository;
        private IMapper _mapper;

        public TimeTableController(ITimetableRepository timetableRepository, IMapper mapper)
        {
            _timetableRepository = timetableRepository;
            _mapper = mapper;
        }

        [HttpGet("student/{id}")]
        public  IActionResult GetScheduleForStudent(int id, DateTime from, DateTime to)
        {
            DateTime dateFrom = DateTime.Parse(HttpUtility.UrlDecode(from.ToString()));
            DateTime dateTo = DateTime.Parse(HttpUtility.UrlDecode(to.ToString()));
            var listAttendances = _timetableRepository.GetSchedulesByStudentId(id, dateFrom, dateTo);
            var resultMapping = _mapper.Map<List<Attendance>, List<AttendanceDTO>>(listAttendances);
            return Ok(resultMapping);
        }

        [HttpGet("instructor/{id}")]
        public IActionResult GetScheduleForInstructor(int id, DateTime from, DateTime to)
        {
            DateTime dateFrom = DateTime.Parse(HttpUtility.UrlDecode(from.ToString()));
            DateTime dateTo = DateTime.Parse(HttpUtility.UrlDecode(to.ToString()));
            var listAttendances = _timetableRepository.GetSchedulesByInstructorId(id, dateFrom, dateTo);
            var resultMapping = _mapper.Map<List<Schedule>, List<ScheduleDTO>>(listAttendances);
            return Ok(resultMapping);
        }


        [HttpGet("statistics")]
        public async Task<IActionResult> GetScheduleForInstructor(int courseId, int id)
        {
            var listAttendance = _timetableRepository.GetStatisticsAttendance(id, courseId);
            var resultMapping = _mapper.Map<List<AttendanceDTO>>(listAttendance);
            var listStatisticAttendance = resultMapping.Select(rs => new Statistics_Attendance
            {
                CourseName = rs.ScheduleDTONav.Course.Code,
                RollNumber = rs.Student.RoleNumber,
                StudentName = rs.Student.Name,
                Attendances = listAttendance.Where(s => s.StudentId == rs.StudentId).Select(c => new AttendanceDTO
                {
                    StudentId = c.StudentId,
                    ScheduleId = c.ScheduleId,
                    DateAttended = c.DateAttended,
                    Status = c.Status,
                    Comment = c.Comment,
                }).ToList(),
                Percentage = TimetableManagement.Instance.getNumberIsAllowedAbsent((int)listAttendance.Where(su => su.StudentId == rs.StudentId).Select(ps => ps.Schedule.Course.Subject.ManageSlot).FirstOrDefault(), resultMapping.Where(su => su.StudentId == rs.StudentId).Count(cu => cu.Status == 1)),
                Summary = resultMapping.Where(su => su.StudentId == rs.StudentId).Count(cu => cu.Status == 1),
            }).ToList();
            var groupedStatistics = listStatisticAttendance.GroupBy(gp => gp.RollNumber)
                                                .Select(grp => grp.First())
                                                .ToList();
            return Ok(groupedStatistics);
        }

        [HttpGet("GetSchedules")]
        public IActionResult GetAllSchedule()
        {
            var listSchedules = _mapper.Map<List<ScheduleDTO>>(_timetableRepository.GetSchedules());
            return Ok(listSchedules);
        }


        [HttpGet("GetDatesByCourseInstructor")]
        public async Task<IActionResult> GetDates(int courseId, int id)
        {
            var listSchedules = _mapper.Map<List<ScheduleDTO>>(_timetableRepository.GetSchedules()).Where(s => s.CourseId == courseId && s.InstructorId == id).Select(c => new
            {
                date = c.Date,
            }).ToList();
            return Ok(listSchedules);
        }
    }
}
