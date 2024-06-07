using FAP_BE.Models;

namespace FAP_BE.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int SubjectId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual SubjectDTO Subject { get; set; } = null!;
    }
}
