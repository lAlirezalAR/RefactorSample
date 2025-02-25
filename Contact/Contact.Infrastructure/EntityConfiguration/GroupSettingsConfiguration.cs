using Contact.Domain.AggregatesModel.GroupAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Infrastructure.EntityConfiguration
{
    public class GroupSettingsConfiguration : IEntityTypeConfiguration<GroupSettings>
    {
        public void Configure(EntityTypeBuilder<GroupSettings> builder)
        {
            builder.ToTable("GroupSettings");

            builder.HasKey(x => x.Id);
            builder.Property(g => g.GroupId).IsRequired();
            builder.Property(g => g.AutoRegister).IsRequired();
            builder.Property(g => g.AutoRegisterCancel).IsRequired();
            builder.Property(g => g.AutoRegisterKeyWord);
            builder.Property(g => g.AutoRegisterLineNumber);
            builder.Property(g => g.AutoRegisterMessage);
            builder.Property(g => g.AutoRegisterCancelKeyWord);
            builder.Property(g => g.AutoRegisterCancelLineNumber);
            builder.Property(g => g.AutoRegisterCancelMessage);

            builder.HasData(Seed());
        }
        private static IEnumerable<GroupSettings> Seed()
        {
            return new List<GroupSettings>
            {
                //Seed Here
            };
        }
    }
}
