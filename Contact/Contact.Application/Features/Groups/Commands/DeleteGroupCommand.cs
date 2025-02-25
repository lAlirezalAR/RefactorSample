using Contact.Application.Features.GroupSettings.Commands;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Services;
using MediatR;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.Groups.Commands
{
    public class DeleteGroupCommand : IRequest<bool>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }

        #region Handler
        public class Handler : IRequestHandler<DeleteGroupCommand, bool>
        {
            private readonly IGroupRepository _repository;
            private readonly IGroupValidatorService _groupValidatorService;
            private readonly IMediator _mediator;

            public Handler(IGroupRepository repository, IGroupValidatorService groupValidatorService, IMediator mediator)
            {
                _repository = repository;
                _groupValidatorService = groupValidatorService;
                _mediator = mediator;
            }

            public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (group == null)
                {
                    throw new AppException("گروه پیدا نشد", System.Net.HttpStatusCode.NotFound);
                }
                if (group.UserId != request.UserId)
                {
                    throw new AppException("شما دسترسی به این گروه ندارید", System.Net.HttpStatusCode.Forbidden);
                }
                await group.Delete(_groupValidatorService);
                var command = new DeleteGroupSettingsCommand() { GroupId = group.Id };
                var result = await _mediator.Send(command, cancellationToken);
                if (await _repository.DeleteAsync(group, cancellationToken) != null)
                    return true;
                return false;
            }
        }
        # endregion Handler
    }
}
