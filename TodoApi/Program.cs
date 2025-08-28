using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// EF Core InMemory database (quick & easy for dev)
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("todos"));
// Add minimal API endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Health endpoint (handy for k8s probes)
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/version", () => Results.Ok(new { version = "2.0.0" }));

app.MapGet("/info", (IConfiguration cfg) =>
{
    var appName = cfg["APP_NAME"] ?? "Todo API";
    return Results.Ok(new { appName });
});

app.MapGet("/secrets-check", (IConfiguration cfg) =>
{
    var key = cfg["API_KEY"] ?? "(missing)";
    return Results.Ok(new { apiKeyLoaded = key != "(missing)" });
});


// CRUD endpoints
app.MapGet("/todos", async (TodoDb db) => await db.Todos.ToListAsync());

app.MapGet("/todos/{id:int}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id) is { } todo ? Results.Ok(todo) : Results.NotFound());

app.MapPost("/todos", async (TodoItem input, TodoDb db) =>
{
    db.Todos.Add(input);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{input.Id}", input);
});

app.MapPut("/todos/{id:int}", async (int id, TodoItem update, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Title = update.Title;
    todo.IsDone = update.IsDone;
    todo.Priority = update.Priority;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todos/{id:int}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();
    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
