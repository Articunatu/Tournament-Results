using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Tournament_Results.Data;
using Tournament_Results.Models;
using Tournament_Results.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IAttendanceDataAccess, AttendanceDataAccess>();
builder.Services.AddScoped<ITournamentDataAccess, TournamentDataAccess>();
builder.Services.AddScoped<IPlayerDataAccess, PlayerDataAccess>();
builder.Services.AddScoped<IPointsDataAccess, PointsDataAccess>();

builder.Services.AddScoped<IPlayerResultsController, PlayerResultsController>();
builder.Services.AddScoped<ITournamentResultsController, TournamentResultsController>();
builder.Services.AddScoped<IATPRankingController, ATPRankingController>();
//builder.Services
//    .AddGraphQLServer()
//    .AddQueryType<Query>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//app.MapGraphQL();

app.Run();