
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using MediatR;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.GroupSettings.Commands
{
    public class DeleteGroupSettingsCommand : IRequest
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int GroupId { get; set; }


        #region Handler
        public class Handler : IRequestHandler<DeleteGroupSettingsCommand, Unit>
        {
            private readonly IGroupSettingsRepository _repository;

            public Handler(IGroupSettingsRepository gruopGroupSettingsRepository)
            {
                _repository = gruopGroupSettingsRepository;
            }

            public async Task<Unit> Handle(DeleteGroupSettingsCommand request, CancellationToken cancellationToken)
            {
                //if (request.GroupId!=null)
                var groupSettings = await _repository.GetByGroupIdAsync(request.GroupId, request.UserId, cancellationToken);
                //var groupSettings = await _repository.GetByIdAsync(request.Id, currentUserId, cancellationToken);
                if (groupSettings == null)
                {
                    throw new AppException("تنظیمات پیدا نشد");
                }
                await _repository.DeleteAsync(groupSettings, cancellationToken);
                await _repository.SaveChangeAsync(cancellationToken);
                return Unit.Value;
            }
        }
        # endregion Handler
    }
}
