using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string InstructorCode { get; set; } = null!;
        public int MetaDataId { get; set; }

        public virtual MetaDatum MetaData { get; set; } = null!;
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
