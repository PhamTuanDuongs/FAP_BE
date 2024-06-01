using FAP_BE.Models;

namespace FAP_BE.DataAccess
{
    public class ScheduleManagement
    {
        private static FAP_PRN231Context _context;
        private static ScheduleManagement _instance;

        private static readonly object _lock = new object();

        public static ScheduleManagement Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ScheduleManagement();
                        _context = new FAP_PRN231Context();
                    }
                    return _instance;
                }
            }
        }

        public bool AddNewSchedule(Schedule schedule)
        {
            try
            {
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
