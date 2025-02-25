namespace Contact.Application.Dto.RegularUser
{
    public class SearchGroupNumbersQueryApiDto
    {
        public string Input { get; set; }
        public int GroupId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
