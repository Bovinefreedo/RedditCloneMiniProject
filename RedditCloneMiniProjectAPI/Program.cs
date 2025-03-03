using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.Design;
using System.Xml.Linq;
using RedditCloneMiniProjectAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


using (var db = new PostContext())
{
    app.MapPost("api/posts", (Post post) =>
    {
        db.Posts.Add(post);
        return Results.Ok(post);
    });
    app.MapPost("api/posts/{postId}/comments", (int postId, Comment comment) =>
    {
        Post? p = db.Posts.FirstOrDefault(x => x.PostId == postId);
        if (p == null)
        {
            return Results.BadRequest("Id was not found");
        }
        else { 
            p.comments.Add(comment);
            return Results.Ok(comment);
        }
    });
    app.MapGet("api/posts", () => {
        return Results.Ok(db.Posts.Find());
    });
    app.MapGet("api/posts/{id}", (int id) => { 
        var result = db.Posts.FirstOrDefault(x => id == x.PostId);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
            return Results.Ok(result);
    });

    app.MapPut("api/posts/{id}/upvote", (int id) =>
    {
        var result = db.Posts.FirstOrDefault(x => id == x.PostId);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else {
            result.score += 1;
            return Results.Ok(result);
        }
    });
    app.MapPut("api/posts/{id}/downvote", (int id) =>
    {
        var result = db.Posts.FirstOrDefault(x => id == x.PostId);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
        {
            result.score -= 1;
            return Results.Ok(result);
        }
    });
    app.MapPut("api/posts/{postId}/comments/{commentId}/upvote", (int postId, int commentId) =>
    {
        var result = db.Posts.FirstOrDefault(x => postId == x.PostId)?
            .comments.FirstOrDefault(x => commentId == x.CommentId);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
        {
            result.score += 1;
            return Results.Ok(result);
        }
    });
    app.MapPut("api/posts/{postId}/comments/{commentId}/downvote", (int postId, int commentId) =>
    {
        var result = db.Posts.FirstOrDefault(x => postId == x.PostId)?
            .comments.FirstOrDefault(x => commentId == x.CommentId);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
        {
            result.score -= 1;
            return Results.Ok(result);
        }
    });
}

//GET:
/// api / posts
/// api / posts /{ id}
//PUT:
/// api / posts /{ id}/ upvote
/// api / posts /{ id}/ downvote
/// api / posts /{ postid}/ comments /{ commentid}/ upvote
/// api / posts /{ postid}/ comments /{ commentid}/ downvote
//POST:
/// api / posts
/// api / posts /{ id}/ comments


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();


