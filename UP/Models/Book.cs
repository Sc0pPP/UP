using System;
using System.Collections.Generic;

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

    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

    public virtual ICollection<FrozenBid> FrozenBids { get; set; } = new List<FrozenBid>();

    public virtual ICollection<ReadingList> ReadingLists { get; set; } = new List<ReadingList>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
