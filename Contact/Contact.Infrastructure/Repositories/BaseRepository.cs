using Microsoft.EntityFrameworkCore;
using Utilities.Framework;
using Utilities.Framework.Contracts;
using Utilities.Framework.Pagination;

namespace Contact.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IWriteRepository<TEntity>, IReadRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly ContactContext dbContext;

        public BaseRepository(ContactContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public virtual IQueryable<TEntity> Table => dbContext.Set<TEntity>();
        public virtual IQueryable<TEntity> TableNoTracking => dbContext.Set<TEntity>().AsNoTracking();
        public TEntity Add(TEntity entity)
        {
            return dbContext.Add(entity).Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            await dbContext.AddAsync(entity, cancellationToken);
            if (saveChanges)
                await SaveChangeAsync(cancellationToken);
            return entity;
        }

        public async Task CommitTransactionAsync(Action action, CancellationToken cancellationToken = default)
        {
            try
            {
                await dbContext.Database.BeginTransactionAsync(cancellationToken);
                action.Invoke();
                await SaveChangeAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public TEntity Delete(TEntity entity)
        {
            return dbContext.Remove(entity).Entity;

        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var deleted = await Task.FromResult(dbContext.Remove(entity).Entity);
            await SaveChangeAsync(cancellationToken);
            return deleted;
        }

        public Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        public TEntity Update(TEntity entity)
        {
            return dbContext.Update(entity).Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var returnable = await Task.FromResult(dbContext.Update(entity).Entity);
            await SaveChangeAsync(cancellationToken);
            return returnable;
        }

        public Task<bool> AnyAsync(CancellationToken cancellationToken)
        {
            return dbContext.Set<TEntity>().AnyAsync(cancellationToken);
        }

        public Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return dbContext.Set<TEntity>().CountAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }
        public Task<IPagedList<TEntity>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken, int limitSize = 100)
        {
            return dbContext.Set<TEntity>().ToPagedListAsync<TEntity>(pageIndex, pageSize, 0, cancellationToken, limitSize);
        }

        public Task<IPagedList<TEntity>> GetPageAsync(IQueryable<TEntity> query, int pageIndex, int pageSize, CancellationToken cancellationToken, int limitSize = 100)
        {
            return query.ToPagedListAsync<TEntity>(pageIndex, pageSize, 0, cancellationToken, limitSize);
        }

    }
}
