namespace Contact.Application.Dto.RegularUser
{
    public class CreateMultipleGroupNumbersCommandApiDto
    {
        public List<string> Numbers { get; set; }
        public int GroupId { get; set; }
    }
}
