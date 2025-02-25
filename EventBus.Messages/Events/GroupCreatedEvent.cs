namespace EventBus.Messages.Events
{
    public class GroupCreatedEvent : IntegrationBaseEvent
    {
        public string GroupName { get; set; }
    }
}
