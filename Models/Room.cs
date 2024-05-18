using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Room
    {
        public Room()
        {
            Sessions = new HashSet<Session>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
