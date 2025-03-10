using Microsoft.EntityFrameworkCore;
using Core.Model;

namespace RedditCloneMiniProjectAPI.Context
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public string DbPath { get; }

        public PostContext(DbContextOptions<PostContext> options) : base(options) { 
        }

    }
}
