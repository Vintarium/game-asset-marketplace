using AssetMarketplace.Domain.Entities;

namespace AssetMarketplace.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
}
