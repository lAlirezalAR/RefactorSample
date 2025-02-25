using Contact.Domain.AggregatesModel.GroupAggregate.Services;
using Contact.Domain.AggregatesModel.UserAggregate.Events;
using Utilities.Framework;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace Contact.Domain.AggregatesModel.GroupAggregate
{
    public class Group : AggregateRoot, IAuditable<int>
    {
        public string Name { get; private set; }
        public int ParentId { get; private set; }
        public int UserId { get; private set; }
        public int CreatedBy { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public int LastModifiedBy { get; private set; }

        public DateTime? LastModifiedDate { get; private set; }

        public GroupSettings GroupSettings { get; private set; }

        private readonly List<GroupNumber> _groupNumbers;
        public IReadOnlyCollection<GroupNumber> GroupNumbers => _groupNumbers;


        public Group()
        {
            _groupNumbers = new List<GroupNumber>();
        }

        public Group(string name, int parentId, int userId)
        {
            Guard.AgainstNullOrEmpty(name, "عنوان الزامی است");
            Guard.AgainstNullValue(parentId, "شناسه سرگروه الزامی است");
            Guard.AgainstNullValue(userId, "شناسه کاربر الزامی است");
            Name = name;
            ParentId = parentId;
            UserId = userId;
        }
        public void Edit(string name, int parentId, int userId)
        {
            Guard.AgainstNullOrEmpty(name, "عنوان الزامی است");
            Guard.AgainstNullValue(parentId, "شناسه سرگروه الزامی است");
            Guard.AgainstNullValue(userId, "شناسه کاربر الزامی است");
            Name = name;
            ParentId = parentId;
            UserId = userId;
        }

        public async Task Create(int maxGroupCountPerUser, IGroupValidatorService validatorService)
        {
            await validatorService.CheckParentIdIsOwned(ParentId, UserId);
            await validatorService.CheckIfMaxGroupCountIsReached(maxGroupCountPerUser, UserId);
            AddEvent(new GroupCreatedDomainEvent(this.Name));
        }
        public async Task Update(IGroupValidatorService validatorService)
        {
            await validatorService.CheckParentIdIsOwned(ParentId, UserId);

            //TODO
        }
        public async Task Delete(IGroupValidatorService validatorService)
        {
            await validatorService.CheckGroupHaveNumber(Id);

            //TODO
        }

    }
}
