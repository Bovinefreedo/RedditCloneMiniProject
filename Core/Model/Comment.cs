namespace Core.Model;

public record Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public User User { get; set; } = new User();
    public int score { get; set; }
    public DateTime datePosted { get; set; } = DateTime.Now;
    public Comment(string content = "", int upvotes = 0, int downvotes = 0, User user = null)
    {
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        User = user;
    }
    public Comment() {
        Id = 0;
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
    }
}
