
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.UserAggregate.Events;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace Contact.Application.Features.Events
{
    public class GroupCreatedDomainEventHandler
                    : INotificationHandler<GroupCreatedDomainEvent>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public GroupCreatedDomainEventHandler(IGroupRepository userRepository, IPublishEndpoint publishEndpoint)
        {
            _groupRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }
        public async Task Handle(GroupCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // var _group = await _groupRepository.GetByIdAsync(notification.GroupName, cancellationToken);
            var eventMessage = new GroupCreatedEvent() { GroupName= notification.GroupName };

            await _publishEndpoint.Publish(eventMessage, cancellationToken);
        }
       
    }
}
