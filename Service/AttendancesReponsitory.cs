using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Repository;
using FAP_BE.DataAccess;
using FAP_BE.Models;


namespace FAP_BE.Service
{
    public class AttendancesReponsitory : IAttendancesRepository
    {
        
        public Task<IEnumerable<AttendanceResponse>> GetAttendancesByScheduleAsync(int scheduleId, int instructorId)
           => AttendancesManagement.Instance.GetAttendancesByScheduleAsync(scheduleId, instructorId);


        public Task<AttendanceResponse> AddOrUpdateAttendanceAsync(int instructorId, AttendanceRequest attendanceRequest)
            => AttendancesManagement.Instance.AddOrUpdateAttendanceAsync(instructorId, attendanceRequest);


        //public Task<AttendanceResponse> AddOrUpdateAttendanceAsync( AttendanceRequest attendanceRequest)
        // => AttendancesManagement.Instance.AddOrUpdateAttendanceAsync( attendanceRequest);

        public  Task<IEnumerable<AttendanceResponse>> UpdateAttendancesAsync(int instructorId, List<AttendanceRequest> attendanceRequests)
        
           => AttendancesManagement.Instance.UpdateAttendancesAsync(instructorId, attendanceRequests);

        public  Task<IEnumerable<StudentAttendanceDetailResponse>> GetStudentAttendanceDetailsAsync(int studentId)

             => AttendancesManagement.Instance.GetStudentAttendanceDetailsAsync(studentId);

        public Task<IEnumerable<ScheduleDetailsDTO>> GetWeeklyScheduleAsync(int instructorId)
         => AttendancesManagement.Instance.GetWeeklyScheduleAsync(instructorId);
}

}
