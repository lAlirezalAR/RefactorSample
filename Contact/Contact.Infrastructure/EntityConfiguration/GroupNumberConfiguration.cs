using Contact.Domain.AggregatesModel.GroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Infrastructure.EntityConfiguration
{
    public class GroupNumberConfiguration : IEntityTypeConfiguration<GroupNumber>
    {
        public void Configure(EntityTypeBuilder<GroupNumber> builder)
        {
            builder.ToTable("GroupNumbers");

            builder.Property(g => g.Number).HasMaxLength(15).IsRequired();
            builder.Property(g => g.GroupId).HasMaxLength(200).IsRequired();
            builder.Property(g => g.FirstName).HasMaxLength(200);
            builder.Property(g => g.LastName).HasMaxLength(200);
            builder.Property(g => g.Email).HasMaxLength(100);
            builder.Property(g => g.CityName).HasMaxLength(200);
            builder.Property(g => g.Company).HasMaxLength(200);
            builder.Property(g => g.Gender).HasMaxLength(20);
            builder.Property(g => g.Description).HasMaxLength(300);
            builder.Property(g => g.BirthDate);
            builder.Property(g => g.Status).IsRequired();

            builder.HasData(Seed());
        }
        private static IEnumerable<GroupNumber> Seed()
        {
            return new List<GroupNumber>
            {
                //Seed Here
            };
        }
    }
}
