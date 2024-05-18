using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Group
    {
        public Group()
        {
            Sessions = new HashSet<Session>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string GroupCode { get; set; } = null!;
        public string GroupName { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
