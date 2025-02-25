using AutoMapper;
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Enums;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Utilities.Framework.Exceptions;
using GA = Contact.Domain.AggregatesModel.GroupAggregate;

namespace Contact.Application.Features.GroupSettings.Commands
{
    public class UpdateGroupSettingsCommand : IRequest<bool>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }
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
        public class Handler : IRequestHandler<UpdateGroupSettingsCommand, bool>
        {
            private readonly IMapper _mapper;
            private readonly IGroupSettingsRepository _repository;

            public Handler(IMapper mapper, IGroupSettingsRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<bool> Handle(UpdateGroupSettingsCommand request, CancellationToken cancellationToken)
            {
                var groupSettings = await _repository.GetByIdAsync(request.Id, request.UserId, cancellationToken);
                if (groupSettings == null)
                {
                    throw new AppException("تنظیمات پیدا نشد");
                }
                _mapper.Map(request, groupSettings, typeof(UpdateGroupSettingsCommand), typeof(GA.GroupSettings));
                var result = await _repository.UpdateAsync(groupSettings, cancellationToken);
                return result != null;
            }
        }
        # endregion Handler

        #region Validator
        public class UpdateCommandValidator : AbstractValidator<UpdateGroupSettingsCommand>
        {
            public UpdateCommandValidator()
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
        # endregion Validator
    }
}
