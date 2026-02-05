using asset_marketplace.Application.DTOs;

namespace asset_marketplace.Application.Interfaces;
public interface IUserService
{
    Task<List<UserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellation);
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    Task<UserDto?> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellation);
}
