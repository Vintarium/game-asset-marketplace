using asset_marketplace.Application.DTOs;
using asset_marketplace.Application.Interfaces;
using asset_marketplace.Domain.Entities;
using asset_marketplace.Domain.Interfaces;
using System.Linq;

namespace asset_marketplace.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        public async Task<List<UserResponseDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return users.Select(user => new UserResponseDto(user.Id, user.Email, user.Role))
                .ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: true);

            if (user is null)
            {
                return null;
            }
            return new UserResponseDto(user.Id, user.Email, user.Role);
        }
    }
}
