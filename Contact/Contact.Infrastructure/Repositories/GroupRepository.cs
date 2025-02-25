using Contact.Domain.AggregatesModel.GroupAggregate;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Contact.Infrastructure.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly ContactContext _context;
        public GroupRepository(ContactContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GroupHasNumber(int groupId, CancellationToken cancellationToken)
        {
            return await _context.GroupNumbers.Where(g => g.GroupId == groupId).AnyAsync(cancellationToken);
        }

        public async Task<bool> ParentIsOwned(int parentId, int currentUserId, CancellationToken none)
        {
            return ((await _context.Groups.FindAsync(new object[] { parentId }, cancellationToken: none)).UserId == currentUserId);
        }

        public async Task<bool> MaxGroupCountIsReached(int maxCount, int userId, CancellationToken cancellationToken)
        {
            return await _context.Groups.Where(x => x.UserId == userId).CountAsync(cancellationToken: cancellationToken) >= maxCount;
        }
        //----------------------------------------------------------------------------------------------

        public async Task<IEnumerable<Group>> GetAllAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Groups.Where(g => g.UserId == userId).ToListAsync(cancellationToken);
        }

        async Task<Group> IGroupRepository.GetByIdAsync(int id, int userId, CancellationToken cancellationToken)
        {
            var g = await _context.Groups.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (g != null) if (g.UserId == userId) return g;
            return null;
        }
    }
}
