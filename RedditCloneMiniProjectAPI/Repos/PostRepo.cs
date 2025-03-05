using Core.Model;
using Microsoft.EntityFrameworkCore;
using RedditCloneMiniProjectAPI.Context;

namespace RedditCloneMiniProjectAPI.Repos
{
    public class PostRepo
    {
        private readonly PostContext db;

        public PostRepo(PostContext context)
        {
            db = context;
        }
         
        public async Task<Post> CreatePost(NewPost newPost)
        {
            var post = new Post { Title = newPost.title, Content = newPost.content };
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return post;
        }

        public async Task<List<Post>> GetPosts()
        {
            return await db.Posts.ToListAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await db.Posts.FindAsync(id);
        }

        public async Task<Post?> UpvotePost(int id)
        {
            var post = await db.Posts.FindAsync(id);
            if (post != null)
            {
                post.Score++;
                await db.SaveChangesAsync();
            }
            return post;
        }

        public async Task<Post?> DownvotePost(int id)
        {
            var post = await db.Posts.FindAsync(id);
            if (post != null)
            {
                post.Score--;
                await db.SaveChangesAsync();
            }
            return post;
        }
    }
}

