using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;

var builder = WebApplication.CreateBuilder(args);

//database located in memory
//builder.Services.AddDbContext<ContextTasks>(p => p.UseInMemoryDatabase("TasksDb"));
builder.Services.AddSqlServer<ContextTasks>(builder.Configuration.GetConnectionString("cnTasks"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] ContextTasks dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Database is in memory: "+ dbContext.Database.IsInMemory());
});

app.Run();
