using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ManageSlot { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
