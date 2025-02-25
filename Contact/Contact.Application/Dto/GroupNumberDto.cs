using Contact.Application.Dto.RegularUser;
using Utilities.Framework.Dtos;

namespace Contact.Application.Dto
{
    public class GroupNumberDto : GroupNumberApiDto, IUserIdDto
    {
        public int UserId { get; set; }
    }
}
