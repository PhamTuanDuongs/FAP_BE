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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeSlot { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
