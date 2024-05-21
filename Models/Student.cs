using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }
        public string RoleNumber { get; set; } = null!;
        public int MetaDataId { get; set; }

        public virtual MetaData MetaData { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
