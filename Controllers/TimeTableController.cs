using AutoMapper;
using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
            var listAttendance = await _timetableRepository.GetStatisticsAttendance(id, courseId);
            var resultMapping = _mapper.Map<List<AttendanceDTO>>(listAttendance);

            var attendanceDTOs = resultMapping.Select(rs => new
            {
                rs,
                Attendances = listAttendance
                    .Where(s => s.StudentId == rs.StudentId)
                    .OrderBy(o => o.Schedule.Date)
                    .Select(c => new AttendanceDTO
                    {
                        StudentId = c.StudentId,
                        ScheduleId = c.ScheduleId,
                        DateAttended = c.DateAttended,
                        Status = c.Status,
                        Comment = c.Comment,
                    })
                    .ToList()
            }).ToList();

            var percentageTasks = attendanceDTOs.Select(async dto => new
            {
                dto.rs,
                dto.Attendances,
                Percentage = await TimetableManagement.Instance.getNumberIsAllowedAbsent(
                    listAttendance.Count(su => su.StudentId == dto.rs.StudentId),
                    resultMapping.Count(su => su.StudentId == dto.rs.StudentId && su.Status == 2)
                ),
                Summary = resultMapping.Count(su => su.StudentId == dto.rs.StudentId && su.Status == 2)
            }).ToList();

            var percentageResults = await Task.WhenAll(percentageTasks);

            var listStatisticAttendance = percentageResults.Select(result => new Statistics_Attendance
            {
                CourseName = result.rs.ScheduleDTONav.Course.Code,
                RollNumber = result.rs.Student.RoleNumber,
                StudentName = result.rs.Student.Name,
                Attendances = result.Attendances,
                Percentage = result.Percentage,
                Summary = result.Summary
            }).ToList();

            var groupedStatistics = listStatisticAttendance.GroupBy(gp => gp.RollNumber)
                                                .Select(grp => grp.First())
                                                .ToList();
            return Ok(groupedStatistics);
        }


        [HttpGet("ExportStatisticToExcel")]
        public async Task<IActionResult> ExportStatisticToExcel(int courseId, int id)
        {
            try
            {
                var listAttendance = await _timetableRepository.GetStatisticsAttendance(id, courseId);
                var resultMapping = _mapper.Map<List<AttendanceDTO>>(listAttendance);

                var attendanceDTOs = resultMapping.Select(rs => new
                {
                    rs,
                    Attendances = listAttendance
                        .Where(s => s.StudentId == rs.StudentId)
                        .Select(c => new AttendanceDTO
                        {
                            StudentId = c.StudentId,
                            ScheduleId = c.ScheduleId,
                            DateAttended = c.DateAttended,
                            Status = c.Status,
                            Comment = c.Comment,
                        })
                        .ToList()
                }).ToList();

                var percentageTasks = attendanceDTOs.Select(async dto => new
                {
                    dto.rs,
                    dto.Attendances,
                    Percentage = await TimetableManagement.Instance.getNumberIsAllowedAbsent(
                        listAttendance.Count(su => su.StudentId == dto.rs.StudentId),
                        resultMapping.Count(su => su.StudentId == dto.rs.StudentId && su.Status == 2)
                    ),
                    Summary = resultMapping.Count(su => su.StudentId == dto.rs.StudentId && su.Status == 2)
                }).ToList();

                var percentageResults = await Task.WhenAll(percentageTasks);

                var listStatisticAttendance = percentageResults.Select(result => new Statistics_Attendance
                {
                    CourseName = result.rs.ScheduleDTONav.Course.Code,
                    RollNumber = result.rs.Student.RoleNumber,
                    StudentName = result.rs.Student.Name,
                    Attendances = result.Attendances,
                    Percentage = result.Percentage,
                    Summary = result.Summary
                }).ToList();

                var groupedStatistics = listStatisticAttendance.GroupBy(gp => gp.RollNumber)
                                                .Select(grp => grp.First())
                                                .ToList();

                MemoryStream stream = new MemoryStream();
                _timetableRepository.ExportStatisticToExcel(groupedStatistics, stream);
                stream.Position = 0;

                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "StatisticInfo.xlsx"
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            }).OrderBy(d => DateTime.ParseExact(d.date, "dd/MM", CultureInfo.InvariantCulture)).ToList();
            return Ok(listSchedules);
        }
    }
}
