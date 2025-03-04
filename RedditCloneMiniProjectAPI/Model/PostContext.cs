using Microsoft.EntityFrameworkCore;
using shared.Model;

namespace RedditCloneMiniProjectAPI.Model
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.User).
                HasForeignKey(u => u.User.UserId);
            
        }
    }
}
