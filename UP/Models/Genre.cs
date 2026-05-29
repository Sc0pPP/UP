using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Genre1 { get; set; } = null!;

    public string Description { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}
