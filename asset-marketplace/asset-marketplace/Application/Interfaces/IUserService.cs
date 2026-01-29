using asset_marketplace.Application.DTOs;

namespace asset_marketplace.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<ResponseUserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<ResponseUserDto?> GetByIdAsync(Guid id, CancellationToken cancellation);
        Task<ResponseUserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<ResponseUserDto?> UpdateAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
