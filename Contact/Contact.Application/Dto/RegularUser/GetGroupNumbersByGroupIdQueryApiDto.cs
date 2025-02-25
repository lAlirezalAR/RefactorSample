namespace Contact.Application.Dto.RegularUser
{
    public class GetGroupNumbersByGroupIdQueryApiDto
    {
        public int GroupId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
