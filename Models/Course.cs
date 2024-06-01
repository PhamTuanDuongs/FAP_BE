using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Course
    {
        public Course()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int SubjectId { get; set; }

        public int InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeSlot { get; set; } = null!;
        public string Room {  get; set; } 


        public virtual Subject Subject { get; set; } = null!;
        public virtual Room RoomNavigation { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual Instructor InstructorNavigation { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
