namespace Contact.Application.Dto.RegularUser
{
    public class SetAutoRegisterApiDto
    {
        public int GroupId { get; set; }
        public string AutoRegisterKeyWord { get; set; }
        public string AutoRegisterLineNumber { get; set; }
        public string AutoRegisterMessage { get; set; }
    }
}
