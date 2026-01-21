using asset_marketplace.Domain.Interfaces;
using asset_marketplace.Infrastructure;
using asset_marketplace.Domain.Entities;
using asset_marketplace.Domain.Enums;
using asset_marketplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

    var userOne = new User
    {
        Id = Guid.NewGuid(),
        Email = "one@test.com",
        PasswordHash = "password",
        Role = UserRole.Buyer
    };

    var userTwo = new User
    {
        Id = Guid.NewGuid(),
        Email = "two@test.com",
        PasswordHash = "password",
        Role = UserRole.Seller
    };

    var userThree = new User
    {
        Id = Guid.NewGuid(),
        Email = "three@test.com",
        PasswordHash = "password",
        Role = UserRole.Admin
    };

    await userRepository.AddAsync(userOne);
    await userRepository.AddAsync(userTwo);
    await userRepository.AddAsync(userThree);

    var AllUsers = await userRepository.GetAllAsync();

    foreach (var user in AllUsers)
    {
        Console.WriteLine($"user.Id: {user.Id}, user.Email: {user.Email}, User.Role: {user.Role} , user.CreatedAt: {user.CreatedAt}");
    }

    var UserById = await userRepository.GetByIdAsync(userThree.Id); 

    if (UserById != null)
    {
        Console.WriteLine($"userById.Id: {UserById.Id}, userById.Email: {UserById.Email}, UserById.Role: {UserById.Role} , UserById.CreatedAt: {UserById.CreatedAt}");
    }
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
