namespace RedditCloneMiniProjectAPI.Model
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int score = 0;
    }
}
