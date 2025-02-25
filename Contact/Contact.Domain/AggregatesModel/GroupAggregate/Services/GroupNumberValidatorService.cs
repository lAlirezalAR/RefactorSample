using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Services
{
    public class GroupNumberValidatorService : IGroupNumberValidatorService
    {
        private readonly IGroupNumberRepository _repository;

        public GroupNumberValidatorService(IGroupNumberRepository repository)
        {
            _repository = repository;
        }

        public async Task CheckIfGroupIsOwned(int groupId, int userId)
        {
            if (await _repository.GetUserIdOfAGroup(groupId, CancellationToken.None) != userId)
                throw new AccessDenied("به گروه مورد نظر دسترسی ندارید");
        }

        public async Task CheckMobileDoesntAlreadyExist(string mobile, int groupId)
        {
            if (await _repository.MobileAlreadyExists(mobile, groupId, CancellationToken.None))
                throw new MobileAlreadyExistsException("این شماره تکراریست");
        }

        public async Task CheckIfMaxGroupNumberCountIsReached(int maxCount, int groupId)
        {
            if (await _repository.MaxGroupNumberCountIsReached(maxCount, groupId, CancellationToken.None))
                throw new MaxGroupNumberCountIsReachedException("شماره ی بیشتری نمیتوانید در این گروه اضافه کنید");
        }
    }
}
