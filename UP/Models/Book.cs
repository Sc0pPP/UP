using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Name { get; set; } = null!;

    public int Author { get; set; }

    public int Genre { get; set; }

    public int Review { get; set; }

    public bool IsFrozen { get; set; }

    public string Description { get; set; } = null!;

    public string CoverPath { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual User AuthorNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    [JsonIgnore]
    public virtual ICollection<FrozenBid> FrozenBids { get; set; } = new List<FrozenBid>();
    [JsonIgnore]
    public virtual ICollection<ReadingList> ReadingLists { get; set; } = new List<ReadingList>();
    [JsonIgnore]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    [JsonIgnore]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
