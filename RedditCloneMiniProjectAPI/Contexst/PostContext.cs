using Microsoft.EntityFrameworkCore;
using Core.Model;

namespace RedditCloneMiniProjectAPI.Context
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Comment> comments { get; set; }
        public string DbPath { get; }

        public PostContext() {
            DbPath = "bin/Post.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source={DbPath}");

    }
}
