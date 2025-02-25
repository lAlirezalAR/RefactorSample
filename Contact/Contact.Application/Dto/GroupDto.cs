using Contact.Application.Dto.RegularUser;
using Utilities.Framework.Dtos;

namespace Contact.Application.Dto
{
    public class GroupDto : GroupApiDto, IUserIdDto
    {
        public int UserId { get; set; }
    }
}
