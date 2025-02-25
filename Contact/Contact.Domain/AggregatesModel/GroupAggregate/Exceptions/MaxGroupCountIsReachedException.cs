using Utilities.Framework.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Exceptions
{
    internal class MaxGroupCountIsReachedException : BusinessException
    {
        public MaxGroupCountIsReachedException()
        {

        }
        public MaxGroupCountIsReachedException(string message) : base(message)
        {
        }
    }
}