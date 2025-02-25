using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;
namespace Utilities.Framework
{

    public abstract class AggregateRoot : IDomainEvent, IEntity<int>
    {
        private readonly List<BaseDomainEvent> _events = new();

        public virtual int Id
        {
            get;
            protected set;
        }

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(int id)
        {
            Guard.AgainstNavigateOrZero(id, "id");
            Id = id;
        }

        public IReadOnlyCollection<BaseDomainEvent> GetEvents()
        {
            return _events.AsReadOnly();
        }

        public void AddEvent(BaseDomainEvent domainEvent)
        {
            if (domainEvent != null)
            {
                if (_events.Contains(domainEvent))
                {
                    throw new ArgumentException("Can't add duplicate event");
                }

                _events.Add(domainEvent);
            }
        }

        public void ClearDomainEvents()
        {
            _events?.Clear();
        }


        public override bool Equals(object obj)
        {
            AggregateRoot aggregateRoot = obj as AggregateRoot;
            if (aggregateRoot is null)
            {
                return false;
            }

            if ((object)this == aggregateRoot)
            {
                return true;
            }

            if (GetUnproxiedType(this) != GetUnproxiedType(aggregateRoot))
            {
                return false;
            }

            if (Id.Equals(default(Guid)) || aggregateRoot.Id.Equals(default(Guid)))
            {
                return false;
            }

            return Id.Equals(aggregateRoot.Id);
        }

        public override int GetHashCode()
        {
            return (GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }

        public static bool operator ==(AggregateRoot left, AggregateRoot right)
        {
            if (left is null && right is null)
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot left, AggregateRoot right)
        {
            return !(left == right);
        }

        public static Type GetUnproxiedType(object obj)
        {
            Type type = obj.GetType();
            string text = type.ToString();
            if (text.Contains("Castle.Proxies.") || text.EndsWith("Proxy"))
            {
                return type.BaseType;
            }

            return type;
        }
    }
}
