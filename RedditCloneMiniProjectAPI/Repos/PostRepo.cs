﻿using Core.Model;
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
            User author = db.Users.FirstOrDefault(u => u.Id == newPost.userId)!;
            Post post = new Post {  Title = newPost.title, 
                                    Content = newPost.content, 
                                    User = author, 

                                };
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return post;
        }

        public async Task<Comment> CreateComment(int postId, NewComment newComment) {
            var author = await db.Users.FirstOrDefaultAsync(u => u.Id == newComment.userId)!;
            Comment comment = new Comment { User = author!,
                                            Content = newComment.comment
                                          };
            var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            db.Comments.Add(comment);
            post!.Comments.Add(comment);
            await db.SaveChangesAsync();
            return comment;
        }

        public async Task<Post[]> GetPosts()
        {
            return await db.Posts.ToArrayAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
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

