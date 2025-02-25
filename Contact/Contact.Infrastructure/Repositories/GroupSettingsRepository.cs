using Contact.Domain.AggregatesModel.GroupAggregate;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Contact.Infrastructure.Repositories
{
    public class GroupSettingsRepository : BaseRepository<GroupSettings>, IGroupSettingsRepository
    {
        private readonly ContactContext _context;
        public GroupSettingsRepository(ContactContext context) : base(context)
        {
            _context = context;
        }

        public Task<GroupSettings> GetByGroupIdAsync(int groupId, int userId, CancellationToken cancellationToken = default)
        {
            var gn = _context.GroupSettings.FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);
            if (gn != null)
            {
                return gn;
            }
            return null;
        }

        public async Task<GroupSettings> GetByIdAsync(int id, int userId, CancellationToken cancellationToken)
        {
            var gn = await _context.GroupSettings.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (gn! != null)
            {
                var g = await _context.Groups.FirstOrDefaultAsync(x => x.Id == gn.GroupId && x.UserId == userId, cancellationToken: cancellationToken);
                if (g != null) if (g.Id == gn.GroupId) return gn;
            }

            return null;
        }
    }
}
