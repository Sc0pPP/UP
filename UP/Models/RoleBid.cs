using System;
using System.Collections.Generic;

namespace UP.Models;

public partial class RoleBid
{
    public int RoleBidId { get; set; }

    public int UserId { get; set; }

    public string Bid { get; set; } = null!;

    public int RequestedRoleId { get; set; }

    public bool IsChecked { get; set; }

    public virtual Role RequestedRole { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
