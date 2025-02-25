using Utilities.Framework.Exceptions;

namespace Contact.Domain.AggregatesModel.GroupAggregate.Exceptions
{
    public class GroupNotEmptyException : BusinessException
    {
        public GroupNotEmptyException()
        {

        }
        public GroupNotEmptyException(string message) : base(message)
        {
        }
    }

}
