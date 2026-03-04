using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Interfaces;

namespace AssetMarketplace.Application.Services;

public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<IReadOnlyCollection<UserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

        return users.Select(user => new UserDto { Id = user.Id, Email = user.Email, Role = user.Role, }).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: true);

        return user is not null ? new UserDto { Id = user.Id, Email = user.Email, Role = user.Role } : null;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var user = new User { Email = createUserDto.Email, PasswordHash = passwordHasher.HashPassword(createUserDto.Password), Role = createUserDto.Role };

        await userRepository.AddAsync(user, cancellationToken);

        return new UserDto { Id = user.Id, Email = user.Email, Role = user.Role };
    }
    public async Task<UserDto?> UpdateAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: false);
        if (user is null)
        {
            return null;
        }

        user.Email = updateUserDto.Email;

        await userRepository.UpdateAsync(user, cancellationToken);

        return new UserDto { Id = user.Id, Email = user.Email, Role = user.Role };
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return false;
        }
        await userRepository.DeleteAsync(id, cancellationToken);
        return true;
    }
}
