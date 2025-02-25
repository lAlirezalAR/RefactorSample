using Utilities.Framework.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Exceptions
{
    public class MaxGroupNumberCountIsReachedException : BusinessException
    {
        public MaxGroupNumberCountIsReachedException()
        {

        }
        public MaxGroupNumberCountIsReachedException(string message) : base(message)
        {
        }
    }
}
