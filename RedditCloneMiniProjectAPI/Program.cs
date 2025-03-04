using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.Design;
using System.Xml.Linq;
using RedditCloneMiniProjectAPI.Context;
using Core.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

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
        Post? p = db.Posts.FirstOrDefault(x => x.Id == postId);
        if (p == null)
        {
            return Results.BadRequest("Id was not found");
        }
        else { 
            p.Comments.Add(comment);
            return Results.Ok(comment);
        }
    });
    app.MapGet("api/posts", () => {
        return Results.Ok(db.Posts.Find());
    });
    app.MapGet("api/posts/{id}", (int id) => { 
        var result = db.Posts.FirstOrDefault(x => id == x.Id);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
            return Results.Ok(result);
    });

    app.MapPut("api/posts/{id}/upvote", (int id) =>
    {
        var result = db.Posts.FirstOrDefault(x => id == x.Id);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else {
            //result.score += 1;
            return Results.Ok(result);
        }
    });
    app.MapPut("api/posts/{id}/downvote", (int id) =>
    {
        var result = db.Posts.FirstOrDefault(x => id == x.Id);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
        {
            //result.score -= 1;
            return Results.Ok(result);
        }
    });

    app.MapPut("api/posts/{postId}/comments/{commentId}/upvote", (int postId, int commentId) =>
    {
        var result = db.Posts.FirstOrDefault(x => postId == x.Id)?
            .Comments.FirstOrDefault(x => commentId == x.Id);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
        {
            //result.score += 1;
            return Results.Ok(result);
        }
    });
    app.MapPut("api/posts/{postId}/comments/{commentId}/downvote", (int postId, int commentId) =>
    {
        var result = db.Posts.FirstOrDefault(x => postId == x.Id)?
            .Comments.FirstOrDefault(x => commentId == x.Id);
        if (result == null)
        {
            return Results.BadRequest("invalid id");
        }
        else
        {
            //result.score -= 1;
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


