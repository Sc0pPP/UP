using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP.Models;

public partial class BookGenre
{
    public int BookGenreId { get; set; }
    [JsonIgnore]
    public int BookId { get; set; }
    [JsonIgnore]
    public int GenreId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
