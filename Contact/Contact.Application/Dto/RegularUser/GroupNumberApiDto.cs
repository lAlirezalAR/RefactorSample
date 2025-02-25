using Contact.Domain.AggregatesModel.GroupAggregate.Enums;

namespace Contact.Application.Dto.RegularUser
{
    public class GroupNumberApiDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string Company { get; set; }
        public Gender? Gender { get; set; }
        public string Description { get; set; }
        public DateTime? BirthDate { get; set; }
        public GroupNumberStatus? Status { get; set; }

    }
}
