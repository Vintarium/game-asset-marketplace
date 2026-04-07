using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Domain.Abstractions;

namespace AssetMarketplace.Application.Interfaces;

public interface IUserService
{
    public Task<Result<IReadOnlyCollection<UserDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    public Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellation);
    public Task<Result<UserDto>> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    public Task<Result<UserDto>> UpdateAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellation);
}
