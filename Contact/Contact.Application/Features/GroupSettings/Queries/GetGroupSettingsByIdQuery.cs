using AutoMapper;
using Contact.Application.Dto;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using MediatR;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.GroupSettings.Queries
{
    public class GetGroupSettingsByIdQuery : IRequest<GroupSettingsDto>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetGroupSettingsByIdQuery, GroupSettingsDto>
        {
            private readonly IMapper _mapper;
            private readonly IGroupSettingsRepository _repository;
            public Handler(IMapper mapper, IGroupSettingsRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<GroupSettingsDto> Handle(GetGroupSettingsByIdQuery query, CancellationToken cancellationToken)
            {
                var groupSettings = await _repository.GetByIdAsync(query.Id, query.UserId, cancellationToken);
                if (groupSettings == null)
                {
                    throw new AppException("تنظیمات گروه پیدا نشد", System.Net.HttpStatusCode.NotFound);
                }
                return _mapper.Map<GroupSettingsDto>(groupSettings);
            }
        }
    }
}
