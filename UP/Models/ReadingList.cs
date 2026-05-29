using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class ReadingList
{
    public int Rlid { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public int BookId { get; set; }

    public int ReadingListType { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Rltype ReadingListTypeNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
