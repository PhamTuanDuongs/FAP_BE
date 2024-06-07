using AutoMapper;
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
        public IActionResult GetScheduleForStudent(int id, DateTime from, DateTime to)
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
    }
}


//{
//    "studentId": 1,
//    "scheduleId": 110,
//    "dateAttended": null,
//    "status": 0,
//    "comment": null,
//    "scheduleDTONav": {
//        "id": 110,
//      "instructorCode": "chilp",
//      "courseId": 29,
//      "slot": 3,
//      "date": "2024-06-04T00:00:00",
//      "room": "BE123",
//      "course": {
//            "id": 29,
//        "code": "PRN231",
//        "subjectId": 1,
//        "instructorId": 0,
//        "startDate": "2024-05-07T00:00:00",
//        "endDate": "2024-06-14T00:00:00",
//        "subject": {
//                "id": 1,
//          "code": "PRN231",
//          "name": ".Net",
//          "manageSlot": 20
//        }
//        }
//    }
//}