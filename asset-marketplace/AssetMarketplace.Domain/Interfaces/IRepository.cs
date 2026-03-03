using AssetMarketplace.Domain.Entities;

namespace AssetMarketplace.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken, bool asNoTracking = true);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = true);
    public Task AddAsync(T entity, CancellationToken cancellationToken);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
