using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Validations;

namespace FAP_BE.Repository
{
    public interface IAttendancesRepository
    {
        Task<IEnumerable<AttendanceResponse>> GetAttendancesByScheduleAsync(int scheduleId, int instructorId);
         Task<AttendanceResponse> AddOrUpdateAttendanceAsync(int instructorId, AttendanceRequest attendanceRequest);
        // Task<AttendanceResponse> AddOrUpdateAttendanceAsync(AttendanceRequest attendanceRequest);
        Task<IEnumerable<AttendanceResponse>> UpdateAttendancesAsync(int instructorId, List<AttendanceRequest> attendanceRequests);

        Task<IEnumerable<StudentAttendanceDetailResponse>> GetStudentAttendanceDetailsAsync(int studentId);

        Task<IEnumerable<ScheduleDetailsDTO>> GetWeeklyScheduleAsync(int instructorId);
    }
}


