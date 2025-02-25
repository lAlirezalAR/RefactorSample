
namespace Contact.Domain.AggregatesModel.GroupAggregate.Services
{
    public interface IGroupNumberValidatorService
    {
        Task CheckIfGroupIsOwned(int GroupId, int userId);
        Task CheckMobileDoesntAlreadyExist(string mobile, int groupId);
        Task CheckIfMaxGroupNumberCountIsReached(int maxCount, int groupId);
    }
}