using System;
using System.Collections.Generic;

namespace FAP_BE.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;

        public virtual List<Account> Account { get; set; } = null!;
    }
}
