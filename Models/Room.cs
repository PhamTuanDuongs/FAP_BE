using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Room
    {
        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string Name { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

    }
}
