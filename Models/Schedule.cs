using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }
        public int InstructorId { get; set; }
        public string Room { get; set; } = null!;
        public int CourseId { get; set; }
        public int Slot { get; set; }
        public DateTime Date { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Instructor Instructor { get; set; } = null!;
        public virtual Room RoomNavigation { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
