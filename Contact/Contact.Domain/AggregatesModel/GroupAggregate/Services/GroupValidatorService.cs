using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Services
{
    public class GroupValidatorService : IGroupValidatorService
    {
        private readonly IGroupRepository _repository;
        public GroupValidatorService(IGroupRepository repository)
        {
            _repository = repository;
        }
        public async Task CheckGroupHaveNumber(int groupId)
        {
            if (await _repository.GroupHasNumber(groupId, CancellationToken.None))
                throw new GroupNotEmptyException("گروه تعدادی مخاطب دارد ابتدا آنها را حذف کنید");
        }

        public async Task CheckIfMaxGroupCountIsReached(int maxCount, int userId)
        {
            if (await _repository.MaxGroupCountIsReached(maxCount, userId, CancellationToken.None))
                throw new MaxGroupCountIsReachedException("گروه ی بیشتری نمیتوانید اضافه کنید");
        }

        public async Task CheckParentIdIsOwned(int parentId, int currentUserId)
        {
            if (parentId != 0)
                if (!await _repository.ParentIsOwned(parentId, currentUserId, CancellationToken.None))
                    throw new AccessDenied("به گروه مورد نظر دسترسی ندارید");
        }
    }
}
