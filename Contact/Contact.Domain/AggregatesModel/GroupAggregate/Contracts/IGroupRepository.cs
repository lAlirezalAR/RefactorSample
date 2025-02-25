using Utilities.Framework.Contracts;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Contracts
{
    public interface IGroupRepository : IReadRepository<Group>, IWriteRepository<Group>
    {
        Task<IEnumerable<Group>> GetAllAsync(int userId, CancellationToken cancellationToken = default);
        Task<Group> GetByIdAsync(int id, int userId, CancellationToken cancellationToken = default);
        Task<bool> GroupHasNumber(int groupId, CancellationToken cancellationToken);
        Task<bool> ParentIsOwned(int parentId, int currentUserId, CancellationToken cancellationToken);
        Task<bool> MaxGroupCountIsReached(int maxCount, int userId, CancellationToken cancellationToken);
    }
}
