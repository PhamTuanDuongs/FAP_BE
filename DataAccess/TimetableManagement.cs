using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace FAP_BE.DataAccess
{
    public class TimetableManagement
    {
        private static FAP_PRN231Context _context;
        private static TimetableManagement _instance;

        private static readonly object _lock = new object();
        public static TimetableManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimetableManagement();
                    _context = new FAP_PRN231Context();
                }
                return _instance;
            }
        }


        public List<Attendance> GetSchedulesByStudentId(int studentId, DateTime from, DateTime to)
        {
            using (var context = new FAP_PRN231Context())
            {
                List<Attendance> listAttendance = _context.Attendances.
                               Include(f => f.Schedule).ThenInclude(i => i.Instructor).
                               Include(c => c.Schedule).ThenInclude(co => co.RoomNavigation).
                               Include(d => d.Schedule).ThenInclude(r => r.Course).ThenInclude(s => s.Subject).
                               Where(sc => sc.Schedule.Date >= from && sc.Schedule.Date <= to)
                               .ToList();
                listAttendance = listAttendance.DistinctBy(s => s.ScheduleId).ToList();
                return listAttendance;
            }

        }

        public List<Schedule> GetSchedulesByInstructorId(int instructorId, DateTime from, DateTime to)
        {
            using (var context = new FAP_PRN231Context())
            {
                List<Schedule> listSchedules = _context.Schedules
                                   .Include(i => i.Instructor)
                                   .Include(co => co.RoomNavigation).
                                   Include(r => r.Course).ThenInclude(s => s.Subject).
                                   Where(sc => sc.Date >= from && sc.Date <= to)
                                   .ToList();
                listSchedules = listSchedules.DistinctBy(s => s.Id).ToList();
                return listSchedules;
            }

        }

    }
}
