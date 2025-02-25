using Utilities.Framework.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Exceptions
{
    public class MobileAlreadyExistsException : BusinessException
    {
        public MobileAlreadyExistsException()
        {
        }

        public MobileAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
