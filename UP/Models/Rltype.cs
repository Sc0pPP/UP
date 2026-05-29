using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class Rltype
{
    public int RltypeId { get; set; }

    public string Type { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ReadingList> ReadingLists { get; set; } = new List<ReadingList>();
}
