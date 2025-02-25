using Contact.Domain.AggregatesModel.GroupAggregate.Enums;
using Contact.Domain.AggregatesModel.GroupAggregate.Services;
using Utilities.Framework;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace Contact.Domain.AggregatesModel.GroupAggregate
{
    public class GroupNumber : AggregateRoot, IAuditable<int>
    {
        public string Number { get; private set; }
        public int GroupId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string CityName { get; private set; }
        public string Company { get; private set; }
        public Gender? Gender { get; private set; }
        public string Description { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public GroupNumberStatus Status { get; private set; }

        public int CreatedBy { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public int LastModifiedBy { get; private set; }

        public DateTime? LastModifiedDate { get; private set; }
        public Group Group { get; private set; }

        public GroupNumber(string number, int groupId, string firstName, string lastName, string email, string cityName, string company, Gender? gender, string description, DateTime? birthDate, GroupNumberStatus status = GroupNumberStatus.Inactive)
        {
            Guard.AgainstNullValue(groupId, "گروه الزامی است");
            Guard.AgainstNullOrEmpty(number, "شماره الزامی است");
            Number = number;
            GroupId = groupId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CityName = cityName;
            Company = company;
            Gender = gender;
            Description = description;
            BirthDate = birthDate;
            Status = status;

        }
        public void Edit(string number, int groupId, string firstName, string lastName, string email, string cityName, string company, Gender? gender, string description, DateTime? birthDate, GroupNumberStatus status)
        {
            Guard.AgainstNullValue(groupId, "گروه الزامی است");
            Guard.AgainstNullOrEmpty(number, "شماره الزامی است");
            Number = number;
            GroupId = groupId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CityName = cityName;
            Company = company;
            Gender = gender;
            Description = description;
            BirthDate = birthDate;
            Status = status;

        }

        public async Task Create(int maxGroupNumberCountPerGroup, IGroupNumberValidatorService validatorService, int userId)
        {
            await validatorService.CheckIfGroupIsOwned(GroupId, userId);
            await validatorService.CheckMobileDoesntAlreadyExist(Number, GroupId);
            await validatorService.CheckIfMaxGroupNumberCountIsReached(maxGroupNumberCountPerGroup, GroupId);

            //Number.ToMobile();
        }

        public async Task Update(IGroupNumberValidatorService validatorService, int userId)
        {
            await validatorService.CheckIfGroupIsOwned(GroupId, userId);
            //Number.ToMobile();
        }

        public async Task Delete(IGroupNumberValidatorService validatorService, int userId)
        {
            await validatorService.CheckIfGroupIsOwned(GroupId, userId);
        }

        public void SetStatus(GroupNumberStatus status)
        {
            Status = status;
        }

    }
}
