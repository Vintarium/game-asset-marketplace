using asset_marketplace.Application.DTOs;

namespace asset_marketplace.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task <UserResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellation);
    }
}
