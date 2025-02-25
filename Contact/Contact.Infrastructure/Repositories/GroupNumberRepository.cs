using Contact.Domain.AggregatesModel.GroupAggregate;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Microsoft.EntityFrameworkCore;
using Utilities.Framework.Pagination;

namespace Contact.Infrastructure.Repositories
{
    public class GroupNumberRepository : BaseRepository<GroupNumber>, IGroupNumberRepository
    {
        private readonly ContactContext _context;
        public GroupNumberRepository(ContactContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> MaxGroupNumberCountIsReached(int maxCount, int groupId, CancellationToken none)
        {
            return await _context.GroupNumbers.Where(x => x.GroupId == groupId).CountAsync(cancellationToken: none) >= maxCount;
        }

        public async Task<bool> MobileAlreadyExists(string mobile, int groupId = 0, CancellationToken cancellationToken = default)
        {
            return await _context.GroupNumbers.AnyAsync(u => (u.GroupId == groupId) && (u.Number == mobile), cancellationToken: cancellationToken);
        }
        public async Task<int> GetUserIdOfAGroup(int groupId, CancellationToken cancellationToken)
        {
            return (await _context.Groups.FindAsync(new object[] { groupId }, cancellationToken: cancellationToken)).UserId;
        }
        //---------------------------------------------------------------------------------------------------------

        public async Task<List<GroupNumber>> GetAllAsync(int userId, CancellationToken cancellationToken = default)
        {
            var groups = await _context.Groups
                .Where(g => g.UserId == userId)
                .ToListAsync(cancellationToken);
            List<int> groupIds = new();
            foreach (var group in groups)
            {
                groupIds.Add(group.Id);
            }
            return await _context.GroupNumbers
                .Where(g => groupIds.Contains(g.GroupId))
                .ToListAsync(cancellationToken);
        }

        async Task<GroupNumber> IGroupNumberRepository.GetByIdAsync(int id, int userId, CancellationToken cancellationToken)
        {

            var gn = await _context.GroupNumbers.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (gn != null)
            {
                var g = await _context.Groups.FirstOrDefaultAsync(x => x.Id == gn.GroupId, cancellationToken: cancellationToken);
                if (g != null) if (g.Id == gn.GroupId) return gn;
            }

            return null;
        }

        public async Task<IEnumerable<GroupNumber>> GetByGroupIdAsync(int userId, int id, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (group == null && group.UserId != userId) return null;
            return await _context.GroupNumbers.Where(g => g.GroupId == id).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IPagedList<GroupNumber>> SearchByNumberAndFirstNameAndLastNameAsync(int userId, int groupId, string input, int PageIndex, int PageSize, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);
            //var group = await _context.Groups.FindAsync(new object[] { groupId, cancellationToken }, cancellationToken);
            if (group == null || group.UserId != userId)
            {
                return new PagedList<GroupNumber>();
            }

            var query = _context.GroupNumbers.Where(g => g.GroupId == groupId);
            if (!string.IsNullOrWhiteSpace(input))
            {
                query = query.Where(g => (g.Number.Contains(input)
                                      || g.FirstName.Contains(input)
                                      || g.LastName.Contains(input)));
            }

            return await GetPageAsync(query, PageIndex, PageSize, cancellationToken);
        }

        public async Task<IPagedList<GroupNumber>> GetAllAsync(int userId, int groupId, int PageIndex, int PageSize, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(new object[] { groupId }, cancellationToken: cancellationToken);
            if (group == null || group.UserId != userId)
            {
                return new PagedList<GroupNumber>();
            }
            var query = _context.GroupNumbers.Where(x => x.GroupId == groupId);

            return await GetPageAsync(query, PageIndex, PageSize, cancellationToken);
        }
    }
}
