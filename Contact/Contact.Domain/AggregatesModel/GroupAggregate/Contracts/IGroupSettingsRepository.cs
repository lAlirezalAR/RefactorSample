using Utilities.Framework.Contracts;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Contracts
{
    public interface IGroupSettingsRepository : IReadRepository<GroupSettings>, IWriteRepository<GroupSettings>
    {
        Task<GroupSettings> GetByIdAsync(int id, int userId, CancellationToken cancellationToken = default);
        Task<GroupSettings> GetByGroupIdAsync(int GroupId, int userId, CancellationToken cancellationToken = default);

    }
}
