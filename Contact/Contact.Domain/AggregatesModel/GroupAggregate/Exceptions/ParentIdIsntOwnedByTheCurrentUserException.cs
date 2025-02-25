using Utilities.Framework.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Exceptions
{
    public class AccessDenied : BusinessException
    {
        public AccessDenied()
        {

        }
        public AccessDenied(string message) : base(message)
        {
        }
    }
}
