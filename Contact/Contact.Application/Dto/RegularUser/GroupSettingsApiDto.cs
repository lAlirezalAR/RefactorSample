using Contact.Domain.AggregatesModel.GroupAggregate.Enums;

namespace Contact.Application.Dto.RegularUser
{
    public class GroupSettingsApiDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public AutoRegisterStatus? AutoRegister { get; set; }
        public string AutoRegisterKeyWord { get; set; }
        public string AutoRegisterLineNumber { get; set; }
        public string AutoRegisterMessage { get; set; }
        public AutoRegisterStatus? AutoRegisterCancel { get; set; }
        public string AutoRegisterCancelKeyWord { get; set; }
        public string AutoRegisterCancelLineNumber { get; set; }
        public string AutoRegisterCancelMessage { get; set; }

    }
}
