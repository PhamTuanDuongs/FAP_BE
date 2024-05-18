using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Session
    {
        public Session()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int SessionId { get; set; }
        public string GroupCode { get; set; } = null!;
        public string InstructorCode { get; set; } = null!;
        public string Room { get; set; } = null!;
        public string CourseCode { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool SessionStatus { get; set; }

        public virtual Course CourseCodeNavigation { get; set; } = null!;
        public virtual Group GroupCodeNavigation { get; set; } = null!;
        public virtual Instructor InstructorCodeNavigation { get; set; } = null!;
        public virtual Room RoomNavigation { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
