using System;
using System.Collections.Generic;

namespace UP.Models;

public partial class FrozenBid
{
    public int FrozenBidId { get; set; }

    public int? UserId { get; set; }

    public string Bid { get; set; } = null!;

    public int? BookId { get; set; }

    public int? ReviewId { get; set; }

    public int? ReportedUserId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Review? Review { get; set; }

    public virtual User? User { get; set; }
}
