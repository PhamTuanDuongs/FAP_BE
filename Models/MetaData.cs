using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class MetaData
    {
        public int MetaDataId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Email { get; set; } = null!;
        public string? Image { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Instructor Instructor { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
