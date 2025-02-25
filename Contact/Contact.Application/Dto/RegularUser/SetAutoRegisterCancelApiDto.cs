namespace Contact.Application.Dto.RegularUser
{
    public class SetAutoRegisterCancelApiDto
    {
        public int GroupId { get; set; }
        public string AutoRegisterCancelKeyWord { get; set; }
        public string AutoRegisterCancelLineNumber { get; set; }
        public string AutoRegisterCancelMessage { get; set; }
    }
}
