using Microsoft.EntityFrameworkCore;
using RedditCloneMiniProjectAPI.Model;

namespace RedditCloneMiniProjectAPI.Repos
{
    public class PostRepo
    {
        PostContext db = new PostContext();

        public Post CreatePost(Post post) { 
            db.Posts.Add(post);
            return post;
        }

        public Comment? CreateComment(int postId, Comment comment) {
            var result = db.Posts.FirstOrDefault(post => post.PostId == postId);
            if (result == null)
                return null;
            else { 
                result.comments.Add(comment);
                return comment;
            }
        }

        public List<Post> GetAllPosts() { 
            return db.Posts.ToList();
        }

        public Post? GetPostById(int postId) {
            return db.Posts.FirstOrDefault(post => post.PostId == postId);
        }

        public Post? ChangeScorePost(int postId, int score) {
            var result = db.Posts.FirstOrDefault(post => post.PostId == postId);
            if (result == null)
                return null;
            else
            {
                result.score += score;
                return result;
            }
        }

    }
}
