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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//connect to db 
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(option => 
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
