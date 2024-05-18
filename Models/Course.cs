using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Course
    {
        public Course()
        {
            Sessions = new HashSet<Session>();
        }

        public string CourseCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
