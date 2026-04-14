using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Domain.Abstractions;
using AssetMarketplace.Domain.Constants;
using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Interfaces;
using AutoMapper;

namespace AssetMarketplace.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IMapper mapper) : IUserService
{
    public async Task<Result<IReadOnlyCollection<UserDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

        var allUsers = mapper.Map<IReadOnlyCollection<UserDto>>(users);

        return Result<IReadOnlyCollection<UserDto>>.Success(allUsers);
    }

    public async Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: true);
        if (user is null)
        {
            return Result<UserDto>.Failure(UserErrors.NotFound);
        }

        var userDto = mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(userDto);

    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByEmailAsync(createUserDto.Email, cancellationToken);
        if (existingUser is not null)
        {
            return Result<UserDto>.Failure(UserErrors.EmailNotUnique);
        }

        var user = mapper.Map<User>(createUserDto);
        user.PasswordHash = passwordHasher.HashPassword(createUserDto.Password);

        await userRepository.AddAsync(user, cancellationToken);

        var userDto = mapper.Map<UserDto>(user);

        return Result<UserDto>.Success(userDto);
    }

    public async Task<Result<UserDto>> UpdateAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: false);
        if (user is null)
        {
            return Result<UserDto>.Failure(UserErrors.NotFound);
        }

        mapper.Map(updateUserDto, user);

        await userRepository.UpdateAsync(user, cancellationToken);

        var userDto = mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(userDto);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        await userRepository.DeleteAsync(id, cancellationToken);
        return Result.Success();
    }
}
