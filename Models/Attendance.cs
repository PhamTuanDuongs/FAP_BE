using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Attendance
    {
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime DateAttended { get; set; }
        public int? Status { get; set; }
        public string? Comment { get; set; }

        public virtual Schedule Schedule { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
