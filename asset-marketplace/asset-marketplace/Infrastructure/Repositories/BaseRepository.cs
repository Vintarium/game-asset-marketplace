using asset_marketplace.Domain.Entities;
using asset_marketplace.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace asset_marketplace.Infrastructure.Repositories;
public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public Task<List<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return _dbSet
            .AsNoTracking()
            .OrderBy(entity => entity.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        if (entity is not null)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
