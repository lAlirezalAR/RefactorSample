namespace Utilities.Framework.Contracts
{
    public interface IWriteRepository<TEntity> where TEntity : AggregateRoot
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveChanges = true);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        TEntity Delete(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task SaveChangeAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(Action action, CancellationToken cancellationToken = default);
    }
}
