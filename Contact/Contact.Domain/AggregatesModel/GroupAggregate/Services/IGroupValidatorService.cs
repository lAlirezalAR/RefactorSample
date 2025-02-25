namespace Contact.Domain.AggregatesModel.GroupAggregate.Services
{
    public interface IGroupValidatorService
    {
        Task CheckGroupHaveNumber(int groupId);
        Task CheckParentIdIsOwned(int parentId, int currentUserId);
        Task CheckIfMaxGroupCountIsReached(int maxGroupCountPerUser, int userId);
    }
}
