using Contact.Domain.AggregatesModel.GroupAggregate;
using Contact.Infrastructure.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using Utilities.Framework;
using Utilities.Framework.Contracts;

namespace Contact.Infrastructure
{
    public class ContactContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMediator _mediator;
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupNumber> GroupNumbers { get; set; }
        public virtual DbSet<GroupSettings> GroupSettings { get; set; }
        public ContactContext(DbContextOptions<ContactContext> options, IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            _alterDeletableEntities();
            _setAudtingValues();
            await _mediator.DispatchDomainEventsAsync(this);
            var entiries = await base.SaveChangesAsync(cancellationToken);


            return entiries;
        }
        private void _alterDeletableEntities()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ISoftDeletable<int> || entry.Entity is ISoftDeletable<long>)
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["Deleter"] = !string.IsNullOrEmpty(userId) ? int.Parse(userId) : 0;
                        entry.CurrentValues["DeletedAt"] = DateTime.Now;
                    }
                    else
                    {
                        entry.CurrentValues["DeletedAt"] = entry.OriginalValues["DeletedAt"];
                    }
                }
            }
        }

        private void _setAudtingValues()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in ChangeTracker.Entries())
            {

                if (entry.Entity is IAuditable<int> || entry.Entity is IAuditable<long>)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedDate"] = DateTime.Now;
                            entry.CurrentValues["CreatedBy"] = !string.IsNullOrEmpty(userId) ? int.Parse(userId) : 0;
                            entry.CurrentValues["LastModifiedDate"] = null;
                            entry.CurrentValues["LastModifiedBy"] = 0;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues["CreatedDate"] = entry.OriginalValues["CreatedDate"];
                            entry.CurrentValues["CreatedBy"] = entry.OriginalValues["CreatedBy"];
                            entry.CurrentValues["LastModifiedDate"] = DateTime.Now;
                            entry.CurrentValues["LastModifiedBy"] = !string.IsNullOrEmpty(userId) ? int.Parse(userId) : 0;
                            break;
                    }
                }
            }
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }


    }
}
