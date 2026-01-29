using asset_marketplace.Application.DTOs;
using asset_marketplace.Application.Interfaces;
using asset_marketplace.Domain.Entities;
using asset_marketplace.Domain.Interfaces;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace asset_marketplace.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        public async Task<List<ResponseUserDto>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return users.Select(user => new ResponseUserDto(user.Id, user.Email, user.Role))
                .ToList();
        }

        public async Task<ResponseUserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: true);

            if (user is null)
            {
                return null;
            }
            return new ResponseUserDto(user.Id, user.Email, user.Role);
        }

        public async Task<ResponseUserDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = createUserDto.Email,
                PasswordHash = HashPassword(createUserDto.PAssword),
                Role = createUserDto.Role
            };

            await _userRepository.AddAsync(user, cancellationToken);

            return new ResponseUserDto(user.Id, user.Email, user.Role);
        }
        public async Task<ResponseUserDto?> UpdateAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken, asNoTracking: true);
            if (user is null)
            {
                return null;
            }

            user.Email = updateUserDto.Email;
            user.Role = updateUserDto.Role;

            await _userRepository.UpdateAsync(user, cancellationToken);

            return new ResponseUserDto(user.Id, user.Email, user.Role);
        }

        private string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            return Convert.ToHexString(bytes);
        }
    }
}
