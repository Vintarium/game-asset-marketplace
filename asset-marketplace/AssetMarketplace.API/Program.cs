using AssetMarketplace.API.Application.Interfaces;
using AssetMarketplace.API.Application.Services;
using AssetMarketplace.API.Domain.Entities;
using AssetMarketplace.API.Domain.Interfaces;
using AssetMarketplace.API.Infrastructure;
using AssetMarketplace.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
