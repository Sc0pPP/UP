using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UP.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string MidleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Role { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool IsFrozen { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<FrozenBid> FrozenBids { get; set; } = new List<FrozenBid>();

    public virtual ICollection<ReadingList> ReadingLists { get; set; } = new List<ReadingList>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<RoleBid> RoleBids { get; set; } = new List<RoleBid>();

    public virtual Role RoleNavigation { get; set; } = null!;
    [NotMapped]
    public string fio
    {
        get
        {
            return LastName+" "+FirstName+" "+MidleName;
        }
    }
}
