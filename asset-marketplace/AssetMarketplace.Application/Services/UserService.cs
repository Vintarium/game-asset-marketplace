using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Interfaces;
using AutoMapper;

namespace AssetMarketplace.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IMapper mapper) : IUserService
{
    public async Task<IReadOnlyCollection<UserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

        return mapper.Map<IReadOnlyCollection<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: true);

        return mapper.Map<UserDto>(user);

    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var user = new User { Email = createUserDto.Email, PasswordHash = passwordHasher.HashPassword(createUserDto.Password), Role = createUserDto.Role };

        await userRepository.AddAsync(user, cancellationToken);

        return mapper.Map<UserDto>(user);

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

        return mapper.Map<UserDto?>(user);

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
