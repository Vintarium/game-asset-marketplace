using AssetMarketplace.Application.DTOs;

namespace AssetMarketplace.Application.Interfaces;

public interface IUserService
{
    public Task<IReadOnlyCollection<UserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellation);
    public Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    public Task<UserDto?> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellation);
}
