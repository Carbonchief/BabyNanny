using BabyNanny.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<BabyNannyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/children", async (Child child, BabyNannyContext db) =>
{
    db.Children.Add(child);
    await db.SaveChangesAsync();
    return Results.Created($"/api/children/{child.Id}", child);
});

app.MapPut("/api/children/{id}", async (int id, Child updated, BabyNannyContext db) =>
{
    var child = await db.Children.FindAsync(id);
    if (child is null) return Results.NotFound();
    child.Name = updated.Name;
    child.Birthday = updated.Birthday;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/api/actions", async (BabyAction action, BabyNannyContext db) =>
{
    var exists = await db.Actions.AnyAsync(a =>
        a.ChildId == action.ChildId && a.Started != null && a.Stopped == null);
    if (exists)
    {
        return Results.BadRequest(new { message = "An action is already running for this child." });
    }

    db.Actions.Add(action);
    await db.SaveChangesAsync();
    return Results.Created($"/api/actions/{action.Id}", action);
});

app.MapPut("/api/actions/{id}", async (int id, BabyAction updated, BabyNannyContext db) =>
{
    var action = await db.Actions.FindAsync(id);
    if (action is null) return Results.NotFound();
    action.Type = updated.Type;
    action.Started = updated.Started;
    action.Stopped = updated.Stopped;
    action.FeedingType = updated.FeedingType;
    action.AmountML = updated.AmountML;
    action.BottleType = updated.BottleType;
    action.MealDescription = updated.MealDescription;
    action.DiaperType = updated.DiaperType;
    action.ChildId = updated.ChildId;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
