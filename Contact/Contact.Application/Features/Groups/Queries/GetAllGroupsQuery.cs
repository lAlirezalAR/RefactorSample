using AutoMapper;
using Contact.Application.Dto;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using MediatR;

namespace Contact.Application.Features.Groups.Queries
{
    public class GetAllGroupsQuery : IRequest<List<GroupDto>>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }

        public class Handler : IRequestHandler<GetAllGroupsQuery, List<GroupDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGroupRepository _repository;

            public Handler(IMapper mapper, IGroupRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<List<GroupDto>> Handle(GetAllGroupsQuery query, CancellationToken cancellationToken)
            {
                var groups = (await _repository.GetAllAsync(query.UserId, cancellationToken))
                    .Where(g => g.UserId == query.UserId).OrderByDescending(g => g.Id).ToList();
                if (!groups.Any())
                {
                    //throw new AppException("گروهی پیدا نشد");
                    return new List<GroupDto> { };
                }

                return _mapper.Map<List<GroupDto>>(groups);
            }
        }
    }
}
