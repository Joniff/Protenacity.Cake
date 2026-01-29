namespace Protenacity.Web.Review.Server;

public class ReviewPage
{
    public int Id { get; set; }
    public Guid Key { get; set; }
    public int DaysLeft { get; set; }
    public DateTime ReviewDate { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required IEnumerable<string> Path { get; set; }
}
