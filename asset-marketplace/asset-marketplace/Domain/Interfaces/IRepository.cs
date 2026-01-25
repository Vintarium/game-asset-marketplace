using asset_marketplace.Domain.Entities;

namespace asset_marketplace.Domain.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
