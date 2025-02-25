using MediatR;
using Utilities.Framework;

namespace Contact.Infrastructure.Configuration;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ContactContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.GetEvents != null && x.Entity.GetEvents().Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.GetEvents())
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
