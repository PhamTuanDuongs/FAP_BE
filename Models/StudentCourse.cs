namespace FAP_BE.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public virtual Student StudentNavigation { get; set; } = null!;

        public virtual Course CourseNavigation { get; set; } = null!;
    }
}
