using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendancesRepository _attendancesRepository;

        public AttendancesController(IAttendancesRepository attendancesRepository)
        {
            _attendancesRepository = attendancesRepository;
        }
        [HttpGet("{scheduleId}/{instructorId}")]
        public async Task<IActionResult> GetAttendancesByScheduleAsync(int scheduleId, int instructorId)
        {
            try
            {
                var attendances = await _attendancesRepository.GetAttendancesByScheduleAsync(scheduleId, instructorId);
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("schedules/{instructorId}")]
        public async Task<IActionResult> GetWeeklyScheduleAsync(int instructorId)
        {
            try
            {
                var result = await AttendancesManagement.Instance.GetWeeklyScheduleAsync(instructorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    

    //[HttpPost("{instructorId}")]
    //public async Task<IActionResult> AddOrUpdateAttendanceAsync(int instructorId, [FromBody] AttendanceRequest attendanceRequest)
    //{
    //    try
    //    {
    //        var result = await _attendancesRepository.AddOrUpdateAttendanceAsync(instructorId, attendanceRequest);
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}
    [HttpPost("{instructorId}")]
        public async Task<IActionResult> UpdateAttendancesAsync(int instructorId, [FromBody] List<AttendanceRequest> attendanceRequests)
        {
            try
            {
                var result = await _attendancesRepository.UpdateAttendancesAsync(instructorId, attendanceRequests);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("student/details/{studentId}")]
        public async Task<IActionResult> GetStudentAttendanceDetailsAsync(int studentId)
        {
            try
            {
                var attendanceDetails = await _attendancesRepository.GetStudentAttendanceDetailsAsync(studentId);
                return Ok(attendanceDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}