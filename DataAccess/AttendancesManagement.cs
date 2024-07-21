using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAP_BE.DataAccess
{
    public class AttendancesManagement : IAttendancesRepository
    {
       

        private static FAP_PRN231Context _context;
        private static AttendancesManagement _instance;

        private static readonly object _lock = new object();
        public static AttendancesManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AttendancesManagement();
                    _context = new FAP_PRN231Context();
                }
                return _instance;
            }
        }
        public async Task<IEnumerable<AttendanceResponse>> GetAttendancesByScheduleAsync(int scheduleId, int instructorId)
        {
            var studentAttendances = await _context.Attendances
               .Include(a => a.Schedule)
               .Include(a => a.Student)
               .Where(a => a.ScheduleId == scheduleId && a.Schedule.InstructorId == instructorId)
               .Select(a => new AttendanceResponse
               {
                   StudentId = a.StudentId,
                   StudentName = a.Student.MetaData.Name,
                   RoleNumber = a.Student.RoleNumber,
                   ScheduleId = a.ScheduleId,
                   DateAttended = a.DateAttended,
                   Status = a.Status,
                   Comment = a.Comment
               })
               .ToListAsync();

            return studentAttendances;
        }


        public async Task<AttendanceResponse> AddOrUpdateAttendanceAsync(int instructorId, AttendanceRequest attendanceRequest)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == attendanceRequest.StudentId);
            var scheduleExists = await _context.Schedules.AnyAsync(s => s.Id == attendanceRequest.ScheduleId && s.InstructorId == instructorId);

            if (!studentExists || !scheduleExists)
            {
                throw new ArgumentException("Student or Schedule does not exist.");
            }

            var existingAttendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.StudentId == attendanceRequest.StudentId && a.ScheduleId == attendanceRequest.ScheduleId);

            if (existingAttendance == null)
            {
                throw new ArgumentException("Attendance record does not exist.");
            }

            existingAttendance.Status = attendanceRequest.Status;
            existingAttendance.DateAttended = DateTime.Now;

            await _context.SaveChangesAsync();

            return new AttendanceResponse
            {
                StudentId = attendanceRequest.StudentId,
                StudentName = attendanceRequest.StudentName,
                ScheduleId = attendanceRequest.ScheduleId,
                DateAttended = existingAttendance.DateAttended,
                Status = attendanceRequest.Status,
                Comment = existingAttendance.Comment
            };
        }


        public async Task<IEnumerable<AttendanceResponse>> UpdateAttendancesAsync(int instructorId, List<AttendanceRequest> attendanceRequests)
        {
            var responses = new List<AttendanceResponse>();

          
            var attendancesQuery = _context.Attendances
                .Where(a => a.Schedule.InstructorId == instructorId);

         
            var scheduleIds = attendanceRequests.Select(request => request.ScheduleId).Distinct();
            var schedules = await _context.Schedules
                .Where(schedule => scheduleIds.Contains(schedule.Id))
                .ToListAsync();

           
            var scheduleDict = schedules.ToDictionary(schedule => schedule.Id);

            foreach (var request in attendanceRequests)
            {
                var schedule = scheduleDict.GetValueOrDefault(request.ScheduleId);

                if (schedule != null)
                {
                    // Cập nhật trạng thái của lịch
                    schedule.Status = true;
                    _context.Schedules.Update(schedule);

                    // Cập nhật trạng thái của Attendance
                    var attendance = await attendancesQuery
                        .FirstOrDefaultAsync(a => a.StudentId == request.StudentId && a.ScheduleId == request.ScheduleId);

                    if (attendance != null)
                    {
                        attendance.Status = request.Status;
                        attendance.DateAttended = DateTime.Now;
                        attendance.Comment = request.Comment;

                        responses.Add(new AttendanceResponse
                        {
                            StudentId = request.StudentId,
                            StudentName = request.StudentName,
                            ScheduleId = request.ScheduleId,
                            DateAttended = attendance.DateAttended,
                            Status = request.Status,
                            Comment = attendance.Comment
                        });
                    }
                    else
                    {
                        throw new ArgumentException($"Attendance record not found for StudentId: {request.StudentId} and ScheduleId: {request.ScheduleId}.");
                    }
                }
                else
                {
                    throw new ArgumentException($"Schedule record not found for ScheduleId: {request.ScheduleId}.");
                }
            }

           
            await _context.SaveChangesAsync();

            return responses;
        }


        public async Task<IEnumerable<StudentAttendanceDetailResponse>> GetStudentAttendanceDetailsAsync(int studentId)
        {
            var attendanceDetails = await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .Include(a => a.Student)
                    .ThenInclude(s => s.MetaData)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Course)
                        .ThenInclude(c => c.Subject)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.RoomNavigation)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Instructor) 
                .Select(a => new StudentAttendanceDetailResponse
                {
                    StudentId = a.StudentId,
                    StudentName = a.Student.MetaData.Name,
                    RoleNumber = a.Student.RoleNumber,
                    ScheduleId = a.ScheduleId,
                    DateAttended = a.Schedule.Date.ToString("dd/MM/yyyy"),
                    Status = a.Status.HasValue ? a.Status.Value.ToString() : "NotYet",
                    Comment = a.Comment,
                    CourseName = a.Schedule.Course.Code,
                    SubjectName = a.Schedule.Course.Subject.Name,
                    CourseCode = a.Schedule.Course.Code,
                    RoomName = a.Schedule.RoomNavigation.Name,
                    TimeSlot = a.Schedule.Course.TimeSlot,
                    Slot = a.Schedule.Slot,
                    InstructorName = a.Schedule.Instructor.MetaData.Name
                })
                .ToListAsync();

            return attendanceDetails;
        }



        public async Task<IEnumerable<ScheduleDetailsDTO>> GetWeeklyScheduleAsync(int instructorId)
        {
            try
            {
                // Lấy ngày hiện tại
                var currentDate = DateTime.Now;

                // Lấy toàn bộ lịch dạy của giáo viên trong ngày hiện tại
                var schedules = await _context.Schedules
                    .Where(s => s.InstructorId == instructorId && s.Date.Date == currentDate)
                    .Include(s => s.Course)
                    .Include(s => s.RoomNavigation)
                    .Include(s => s.Instructor)
                    .Select(s => new ScheduleDetailsDTO
                    {
                        Id = s.Id,
                        Date = s.Date,
                        Slot = s.Slot,
                        CourseCode = s.Course.Code,
                        RoomName = s.RoomNavigation.Name,
                        InstructorName = s.Instructor.MetaData.Name,
                        TimeSlot = s.Course.TimeSlot,
                        Status = s.Status,
                    })
                    .OrderBy(s => s.Date)
                    .ThenBy(s => s.Slot)
                    .ToListAsync();

                return schedules;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                throw new Exception("Error fetching schedule data", ex);
            }
        }
    }
}

