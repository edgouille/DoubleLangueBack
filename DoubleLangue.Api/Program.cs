using System;
using DoubleLangue.Infrastructure;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Infrastructure.Interface.Utils;
using DoubleLangue.Infrastructure.Repositories;
using DoubleLangue.Infrastructure.Utils;
using DoubleLangue.Services.Interfaces;
using DoubleLangue.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    // Only for dev TODO Change Later when db model more stable
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Supprime la base existante (supprime aussi les données)
    //dbContext.Database.EnsureDeleted();

    // Recrée la base et toutes les tables à partir des modèles
    //dbContext.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
