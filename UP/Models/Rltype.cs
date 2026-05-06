using System;
using System.Collections.Generic;

namespace UP.Models;

public partial class Rltype
{
    public int RltypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<ReadingList> ReadingLists { get; set; } = new List<ReadingList>();
}
