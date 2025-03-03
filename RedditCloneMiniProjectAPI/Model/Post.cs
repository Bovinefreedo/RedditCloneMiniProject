namespace RedditCloneMiniProjectAPI.Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public User? Author { get; set; }
        public List<Comment> comments { get; set; } = new();
        public int score = 0;

    }
}
