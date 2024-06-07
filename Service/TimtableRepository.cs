using FAP_BE.DataAccess;
using FAP_BE.Models;
using FAP_BE.Repository;

namespace FAP_BE.Service
{
    public class TimtableRepository : ITimetableRepository
    {
        List<Schedule> ITimetableRepository.GetSchedulesByInstructorId(int id, DateTime from, DateTime to) => TimetableManagement.Instance.GetSchedulesByInstructorId(id, from, to);

        List<Attendance> ITimetableRepository.GetSchedulesByStudentId(int id, DateTime from, DateTime to) => TimetableManagement.Instance.GetSchedulesByStudentId(id, from, to);
    }
}
