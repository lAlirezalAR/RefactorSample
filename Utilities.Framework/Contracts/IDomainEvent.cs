namespace Utilities.Framework.Contracts
{
    public interface IDomainEvent
    {
        IReadOnlyCollection<BaseDomainEvent> GetEvents();
        void AddEvent(BaseDomainEvent domainEvent);
    }
}
