using AutoMapper;
using Contact.Domain.AggregatesModel.GroupAggregate.Enums;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Utilities.Framework.Contracts;
using GA = Contact.Domain.AggregatesModel.GroupAggregate;

namespace Contact.Application.Features.GroupSettings.Commands
{
    public class CreateGroupSettingsCommand : IRequest<int>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string AutoRegisterKeyWord { get; set; }
        public string AutoRegisterLineNumber { get; set; }
        public string AutoRegisterMessage { get; set; }
        public string AutoRegisterCancelKeyWord { get; set; }
        public string AutoRegisterCancelLineNumber { get; set; }
        public string AutoRegisterCancelMessage { get; set; }
        [EnumDataType(typeof(AutoRegisterStatus))]
        public AutoRegisterStatus? AutoRegister
        {
            get { return (AutoRegisterStatus)AutoRegisterId; }
            set { AutoRegisterId = (int)value; }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        protected virtual int AutoRegisterId { get; set; }
        [EnumDataType(typeof(AutoRegisterStatus))]
        public AutoRegisterStatus? AutoRegisterCancel
        {
            get { return (AutoRegisterStatus)AutoRegisterCancelId; }
            set { AutoRegisterCancelId = (int)value; }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        protected virtual int AutoRegisterCancelId { get; set; }

        #region Handler
        public class Handler : IRequestHandler<CreateGroupSettingsCommand, int>
        {
            private readonly IMapper _mapper;
            private readonly IWriteRepository<GA.GroupSettings> _gruopSettingsRepository;

            public Handler(IMapper mapper, IWriteRepository<GA.GroupSettings> gruopSettingsRepository)
            {
                _mapper = mapper;
                _gruopSettingsRepository = gruopSettingsRepository;
            }

            public async Task<int> Handle(CreateGroupSettingsCommand request, CancellationToken cancellationToken)
            {
                var groupSettings = _mapper.Map<GA.GroupSettings>(request);
                await _gruopSettingsRepository.AddAsync(groupSettings, cancellationToken);
                //await _gruopSettingsRepository.SaveChangeAsync(cancellationToken);
                return groupSettings.Id;
            }
        }
        #endregion Handler

        #region Validator
        public class CreativeCommandValidator : AbstractValidator<CreateGroupSettingsCommand>
        {
            public CreativeCommandValidator()
            {
                RuleFor(c => c.GroupId)
               .NotEmpty().WithMessage("{GroupId} is required")
               .NotNull();
                //RuleFor(c => c.AutoRegister)
                //    .IsInEnum()
                //    .NotNull().WithMessage("{AutoRegister} is required");
                //RuleFor(c => c.AutoRegisterCancel)
                //    .IsInEnum()
                //    .NotNull().WithMessage("{AutoRegisterCancel} is required");
            }
        }
        #endregion Validator
    }
}
