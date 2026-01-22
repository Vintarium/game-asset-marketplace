using asset_marketplace.Domain.Entities;
using asset_marketplace.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace asset_marketplace.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}
