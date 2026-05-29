using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class Review
{
    public int ReviewId { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }

    public string ReviewText { get; set; } = null!;
    [JsonIgnore]
    public int BookId { get; set; }

    public int Rating { get; set; }

    public bool? IsFrozen { get; set; }

    public virtual Book Book { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<FrozenBid> FrozenBids { get; set; } = new List<FrozenBid>();
    [JsonIgnore]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual User User { get; set; } = null!;
}
