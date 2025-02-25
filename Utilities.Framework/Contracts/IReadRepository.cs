
using Utilities.Framework.Pagination;

namespace Utilities.Framework.Contracts
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<TEntity>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken, int limitSize = 100);
        Task<IPagedList<TEntity>> GetPageAsync(IQueryable<TEntity> query, int pageIndex, int pageSize, CancellationToken cancellationToken, int limitSize = 100);

    }

}
