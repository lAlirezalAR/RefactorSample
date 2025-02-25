using Contact.Application.Dto.RegularUser;
using Utilities.Framework.Dtos;

namespace Contact.Application.Dto
{
    public class GroupSettingsDto : GroupSettingsApiDto, IUserIdDto
    {
        public int UserId { get; set; }
    }
}
