using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Role1 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<RoleBid> RoleBids { get; set; } = new List<RoleBid>();
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
