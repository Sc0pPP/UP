using UP.Models;

public class BidRequest
{
    public BidTypes Type { get; set; }
    public int UserId { get; set; }
    public int? BookId { get; set; }
    public int? ReviewId { get; set; }
    public int? ReportedUserId { get; set; }
    public string Cause { get; set; } = "";
}