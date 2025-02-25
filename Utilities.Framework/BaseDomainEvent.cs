using MediatR;

namespace Utilities.Framework
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
