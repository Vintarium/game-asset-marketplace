using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetMarketplace.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }
}
