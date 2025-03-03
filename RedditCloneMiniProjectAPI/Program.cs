using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.Design;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();


