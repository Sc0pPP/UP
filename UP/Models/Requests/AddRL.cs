namespace UP.Models.Requests;

public class AddRL
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int ReadingListType { get; set; }
}