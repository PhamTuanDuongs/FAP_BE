using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string InstructorCode { get; set; } = null!;
        public int MetaDataId { get; set; }

        public virtual MetaData MetaData { get; set; } = null!;
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
