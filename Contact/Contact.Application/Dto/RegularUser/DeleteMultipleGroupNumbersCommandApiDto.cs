namespace Contact.Application.Dto.RegularUser
{
    public class DeleteMultipleGroupNumbersCommandApiDto
    {
        public List<int> Ids { get; set; }
        public int GroupId { get; set; }
    }
}
