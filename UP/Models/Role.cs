using System;
using System.Collections.Generic;

namespace UP.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<RoleBid> RoleBids { get; set; } = new List<RoleBid>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
