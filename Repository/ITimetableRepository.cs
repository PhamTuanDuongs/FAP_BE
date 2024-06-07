using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface ITimetableRepository
    {

        public List<Attendance> GetSchedulesByStudentId(int id, DateTime from, DateTime to);

        public List<Schedule> GetSchedulesByInstructorId(int id, DateTime from, DateTime to);
    }
}
