using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class FrozenBid
{
    public int FrozenBidId { get; set; }
    [JsonIgnore]
    public int? UserId { get; set; }

    public string Bid { get; set; } = null!;
    [JsonIgnore]
    public int? BookId { get; set; }
    [JsonIgnore]
    public int? ReviewId { get; set; }
    [JsonIgnore]
    public int? ReportedUserId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Review? Review { get; set; }

    public virtual User? User { get; set; }
}
