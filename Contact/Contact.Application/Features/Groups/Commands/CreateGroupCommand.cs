using AutoMapper;
using Contact.Application.Dto;
using Contact.Application.Features.GroupSettings.Commands;
using Contact.Domain.AggregatesModel.GroupAggregate;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Services;
using FluentValidation;
using MediatR;
using Utilities.Framework;

namespace Contact.Application.Features.Groups.Commands
{
    public class CreateGroupCommand : IRequest<GroupDto>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }


        #region Handler
        public class Handler : IRequestHandler<CreateGroupCommand, GroupDto>
        {
            private readonly IMapper _mapper;
            private readonly IGroupRepository _gruopRepository;
            private readonly IGroupValidatorService _groupValidatorService;
            private readonly IMediator _mediator;

            public Handler(
                IMapper mapper,
                IGroupRepository gruopRepository,
                IGroupValidatorService groupValidatorService,
                IMediator mediator
                )
            {
                _mapper = mapper;
                _gruopRepository = gruopRepository;
                _groupValidatorService = groupValidatorService;
                _mediator = mediator;
            }

            public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var group = new Group(request.Name, request.ParentId, request.UserId);
                var maxGroupCountPerUser = 10;
                await group.Create(maxGroupCountPerUser, _groupValidatorService);
                await _gruopRepository.AddAsync(group, cancellationToken);
                var command = new CreateGroupSettingsCommand() { GroupId = group.Id };
                await _mediator.Send(command, cancellationToken);
                return _mapper.Map<GroupDto>(group);
            }
        }
        # endregion Handler

        #region Validator
        public class CreateCommandValidator : AbstractValidator<CreateGroupCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(c => c.Name)
               .NotEmpty().WithMessage("{Name} is required")
               .NotNull().MaximumLength(200).WithMessage("{Name} must not exceed 200 characters. ");
            }
        }
        # endregion Validator
    }
}
