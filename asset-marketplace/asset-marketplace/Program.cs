using asset_marketplace.Domain.Interfaces;
using asset_marketplace.Infrastructure;
using asset_marketplace.Domain.Entities;
using asset_marketplace.Domain.Enums;
using asset_marketplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using asset_marketplace.Application.Interfaces;
using asset_marketplace.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
    CancellationToken cancellationToken = new CancellationToken();

    Console.WriteLine("START TO CREATE USERS: \n");

    for (int i = 0; i < 50; i++)
    {
        await userRepository.AddAsync(new User
        {
            Id = Guid.NewGuid(),
            Email = i.ToString() + "@test.com",
            PasswordHash = "password",
            Role = UserRole.Buyer
        }, cancellationToken);
    }

    var userOne = new User
    {
        Id = Guid.NewGuid(),
        Email = "one@test.com",
        PasswordHash = "password",
        Role = UserRole.Buyer
    }; 

    var AllUsers = await userRepository.GetAllAsync(1, 5, cancellationToken);
    int count = 1;

    Console.WriteLine("\n GETT ALL USERS FROM DATABASE AND SHOW THEM: \n");

    foreach (var user in AllUsers)
    {
        Console.WriteLine($" user.Id: {user.Id} ,\n user.Email: {user.Email} ,\n User.Role: {user.Role} ,\n user.CreatedAt: {user.CreatedAt} \n");
        Console.WriteLine($"user number: {count} \n");
        count++;
    }

    Console.WriteLine("\n GET USER BY ID AND SHOW HIM: \n");

    var UserById = await userRepository.GetByIdAsync(userOne.Id, cancellationToken);

    if (UserById != null)
    {
        Console.WriteLine($" userById.Id: {UserById.Id} ,\n userById.Email: {UserById.Email} ,\n UserById.Role: {UserById.Role} ,\n UserById.CreatedAt: {UserById.CreatedAt} \n");
    }

    Console.WriteLine("\n GET USER BY ID, UPDATE HIM AND SHOW HIM: \n");

    var UpdateUser = await userRepository.GetByIdAsync(userOne.Id, cancellationToken);

    if (UpdateUser != null)
    {
        UpdateUser.Email = "EmailUpdated@gmail.com";
    }

    var userThreeAfterUpdate = await userRepository.GetByIdAsync(userOne.Id, cancellationToken);
    if (userThreeAfterUpdate != null)
    {
        Console.WriteLine($" userThreeAfterUpdate: {userThreeAfterUpdate.Email}");
    }

    Console.WriteLine("\n DELETE USER BY ID AND SHOW ALL USERS WITHOUT DELETED USER: \n");

    await userRepository.DeleteAsync(userOne.Id, cancellationToken);

    var AllUsersAfterDeleted = await userRepository.GetAllAsync(1, 10, cancellationToken);

    foreach (var user in AllUsersAfterDeleted)
    {
        Console.WriteLine($" user.Id: {user.Id} ,\n user.Email: {user.Email} ,\n User.Role: {user.Role} ,\n user.CreatedAt: {user.CreatedAt}  \n");
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
