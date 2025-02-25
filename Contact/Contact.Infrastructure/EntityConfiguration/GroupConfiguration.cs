using Contact.Domain.AggregatesModel.GroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Infrastructure.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.HasKey(x => x.Id);
            builder.Property(g => g.Name).HasMaxLength(200).IsRequired();
            builder.Property(g => g.ParentId).IsRequired();
            builder.Property(g => g.UserId).IsRequired();

            builder.HasMany<GroupNumber>(g => g.GroupNumbers)
                .WithOne(pt => pt.Group)
                .HasForeignKey(pt => pt.GroupId);
            builder.HasOne(a => a.GroupSettings)
            .WithOne(b => b.Group)
            .HasForeignKey<GroupSettings>(b => b.GroupId);

            builder.HasData(Seed());
        }
        private static IEnumerable<Group> Seed()
        {
            return new List<Group>
            {
                //Seed Here
            };
        }
    }
}
