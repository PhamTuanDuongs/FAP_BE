using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;

namespace FAP_BE.Service
{
    public class TimtableRepository : ITimetableRepository
    {
        public bool ExportStatisticToExcel(List<Statistics_Attendance> list, Stream stream) => TimetableManagement.Instance.ExportStatisticToExcel(list, stream);

        List<Schedule> ITimetableRepository.GetSchedules() => TimetableManagement.Instance.GetSchedules();

        List<Schedule> ITimetableRepository.GetSchedulesByInstructorId(int id, DateTime from, DateTime to) => TimetableManagement.Instance.GetSchedulesByInstructorId(id, from, to);

        List<Attendance> ITimetableRepository.GetSchedulesByStudentId(int id, DateTime from, DateTime to) => TimetableManagement.Instance.GetSchedulesByStudentId(id, from, to);

        Task<List<Attendance>> ITimetableRepository.GetStatisticsAttendance(int id, int courseId) => TimetableManagement.Instance.GetStatisticsAttendance(id, courseId);
    }
}
