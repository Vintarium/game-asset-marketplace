using AssetMarketplace.API.Application.DTOs;

namespace AssetMarketplace.API.Application.Interfaces;
public interface IUserService
{
    Task<IReadOnlyCollection<UserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellation);
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    Task<UserDto?> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellation);
}
