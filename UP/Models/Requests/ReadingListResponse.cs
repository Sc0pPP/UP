using System.Collections.Generic;
using UP.Models;

namespace UP.Models.Requests;

public class ReadingListResponse
{
    public int Rlid { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int ReadingListType { get; set; }
    public Book Book { get; set; } = null!;
    public List<string> Genres { get; set; } = new();
}
