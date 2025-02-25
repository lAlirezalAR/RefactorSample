using AutoMapper;
using Contact.Application.Dto;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using MediatR;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.Groups.Queries
{
    public class GetGroupByIdQuery : IRequest<GroupDto>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetGroupByIdQuery, GroupDto>
        {
            private readonly IMapper _mapper;
            private readonly IGroupRepository _repository;

            public Handler(IMapper mapper, IGroupRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<GroupDto> Handle(GetGroupByIdQuery query, CancellationToken cancellationToken)
            {
                var group = await _repository.GetByIdAsync(query.Id, query.UserId, cancellationToken);
                if (group == null)
                {
                    throw new AppException("گروهی پیدا نشد");
                }
                return _mapper.Map<GroupDto>(group);
            }
        }
    }
}
