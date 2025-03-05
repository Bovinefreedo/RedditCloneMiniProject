using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.Design;
using System.Xml.Linq;
using RedditCloneMiniProjectAPI.Context;
using Core.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using RedditCloneMiniProjectAPI.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PostRepo>();

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

builder.Services.AddScoped<PostRepo>();


builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseCors(AllowSomeStuff);

app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});

app.MapPost("api/posts", async (PostRepo repo, NewPost newPost) =>
{
    Post post = await repo.CreatePost(newPost);
    return Results.Ok(post);
});

app.MapPost("api/posts/{postId}/comments", async (PostRepo repo, int postId, NewComment NewComment) =>
{
    Comment comment = await repo.CreateComment(postId, NewComment);
    return Results.Ok(comment);
});
app.MapGet("api/posts", () =>
{
    return Results.Ok(db.Posts.ToList());
});
app.MapGet("api/posts/{id}", (int id) =>
{
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
    else
    {
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
    app.MapPost("api/posts", async (PostRepo repo, NewPost newPost) =>
Results.Ok(await repo.CreatePost(newPost))
);

});

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


