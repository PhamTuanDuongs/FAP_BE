using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Attendance
    {
        public string StudentCode { get; set; } = null!;
        public int SessionId { get; set; }
        public DateTime DateAttended { get; set; }
        public DateTime Time { get; set; }
        public int AttendanceStatus { get; set; }

        public virtual Session Session { get; set; } = null!;
        public virtual Student StudentCodeNavigation { get; set; } = null!;
    }
}
