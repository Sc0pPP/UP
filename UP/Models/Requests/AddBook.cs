using System.Collections.Generic;

namespace UP.Models.Requests;

public class AddBook
{
    public string Name { get; set; } = "";
    public int Author { get; set; }
    public string Description { get; set; } = "";
    public bool IsFrozen { get; set; }
    public string CoverPath { get; set; } = "";
    public string Content { get; set; } = "";
    public int Review { get; set; }
    public List<int> GenreIds { get; set; } = new();
}
