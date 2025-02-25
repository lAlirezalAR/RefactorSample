using Utilities.Framework.Contracts;
using Utilities.Framework.Pagination;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Contracts
{
    public interface IGroupNumberRepository : IReadRepository<GroupNumber>, IWriteRepository<GroupNumber>
    {

        Task<bool> MobileAlreadyExists(string mobile, int groupId, CancellationToken cancellationToken = default);
        Task<bool> MaxGroupNumberCountIsReached(int maxCount, int groupId, CancellationToken none);
        Task<int> GetUserIdOfAGroup(int groupId, CancellationToken cancellationToken);

        Task<IPagedList<GroupNumber>> GetAllAsync(int userId, int groupId, int PageIndex, int PageSize, CancellationToken cancellationToken);

        Task<GroupNumber> GetByIdAsync(int userId, int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<GroupNumber>> GetByGroupIdAsync(int userId, int Id, CancellationToken cancellationToken);
        Task<IPagedList<GroupNumber>> SearchByNumberAndFirstNameAndLastNameAsync(int userId, int groupId, string input, int PageIndex, int PageSize, CancellationToken cancellationToken);
    }
}
