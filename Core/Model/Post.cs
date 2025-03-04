namespace Core.Model;

public class Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }   
    public User User { get; set; }
    public DateTime postedTime {get; set;} = DateTime.Now;
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public Post(User user, string title = "", string content = "") {
        Title = title;
        Content = content;
        User = user;
    }
    public Post() {
        Id = 0;
        Title = "";
        Content = "";
        User = null;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Content: {Content}, Upvotes: {Upvotes}, Downvotes: {Downvotes}, User: {User}";
    }
}