using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public int MetaDataId { get; set; }

        public virtual MetaData MetaData { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
