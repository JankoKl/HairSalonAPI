
using FrizerskiSalon.API.Data;
using FrizerskiSalon.API.Data.Migrations;
using FrizerskiSalon.API.Endpoints;
using FrizerskiSalon.API.Services;
using FrizerskiSalon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var connString = builder.Configuration.GetConnectionString("FrizerskiSalon");
builder.Services.AddScoped<IRadnikService, RadnikService>();

builder.Services.AddDbContext<FrizerskiSalonContext>(options =>
    options.UseSqlite(connString) 
);
var app = builder.Build();



app.MapEndpoints();
app.MigrateDb();
app.Run();


