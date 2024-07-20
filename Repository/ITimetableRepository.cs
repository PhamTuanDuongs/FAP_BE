using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface ITimetableRepository
    {

        public List<Attendance> GetSchedulesByStudentId(int id, DateTime from, DateTime to);

        public List<Schedule> GetSchedulesByInstructorId(int id, DateTime from, DateTime to);
        public List<Attendance> GetStatisticsAttendance(int id, int courseId);
        public List<Schedule> GetSchedules();
        public bool ExportStatisticToExcel(List<Statistics_Attendance> list, Stream stream);
    }
}
