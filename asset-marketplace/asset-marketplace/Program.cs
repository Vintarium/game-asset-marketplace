using asset_marketplace.Domain.Interfaces;
using asset_marketplace.Infrastructure;
using asset_marketplace.Domain.Entities;
using asset_marketplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using asset_marketplace.Application.Interfaces;
using asset_marketplace.Application.Services;

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
