using Utilities.Framework;

namespace Contact.Domain.AggregatesModel.UserAggregate.Events
{
    public class GroupCreatedDomainEvent : BaseDomainEvent
    {
        public string GroupName { get; private set; }
        public GroupCreatedDomainEvent(string userName)
        {
            GroupName = userName;
        }
    }
}
