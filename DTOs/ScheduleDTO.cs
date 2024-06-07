using FAP_BE.Models;

namespace FAP_BE.DTOs
{
    public class ScheduleDTO
    {

        public int Id { get; set; }
        public string InstructorCode { get; set; }
        public int CourseId { get; set; }
        public int Slot { get; set; }
        public string Date { get; set; }
        public string Room { get; set; }

        public bool Status { get; set; }

        public CourseDTO Course { get; set; }
    }
}
