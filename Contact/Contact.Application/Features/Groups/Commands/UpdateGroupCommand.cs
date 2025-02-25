using AutoMapper;
using Contact.Domain.AggregatesModel.GroupAggregate;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Services;
using FluentValidation;
using MediatR;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<bool>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        #region Handler
        public class Handler : IRequestHandler<UpdateGroupCommand, bool>
        {
            private readonly IMapper _mapper;
            private readonly IGroupRepository _repository;
            private readonly IGroupValidatorService _groupValidatorService;

            public Handler(IMapper mapper, IGroupRepository repository, IGroupValidatorService groupValidatorService)
            {
                _mapper = mapper;
                _repository = repository;
                _groupValidatorService = groupValidatorService;
            }

            public async Task<bool> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (group == null)
                {
                    throw new AppException("گروه پیدا نشد");
                }
                if (group.UserId != request.UserId)
                {
                    throw new AppException("شما دسترسی به این گروه ندارید", System.Net.HttpStatusCode.Forbidden);
                }
                _mapper.Map(request, group, typeof(UpdateGroupCommand), typeof(Group));
                await group.Update(_groupValidatorService);
                var result = await _repository.UpdateAsync(group, cancellationToken);
                return result != null;
            }
        }
        # endregion Handler

        #region Validator
        public class UpdateCommandValidator : AbstractValidator<UpdateGroupCommand>
        {
            public UpdateCommandValidator()
            {
                RuleFor(c => c.Id)
               .NotEmpty().WithMessage("{Id} is required")
               .NotNull();
                RuleFor(c => c.Name)
               .NotEmpty().WithMessage("{Name} is required")
               .NotNull()
               .MaximumLength(200).WithMessage("{Name} must not exceed 200 characters. ");


            }
        }
        # endregion Validator
    }
}
