using Contact.Application.Dto.RegularUser;
using Utilities.Framework.Dtos;

namespace Contact.Application.Dto
{
    public class SearchGroupNumbersQueryDto : SearchGroupNumbersQueryApiDto, IUserIdDto
    {
        public int UserId { get; set; }
    }
}
