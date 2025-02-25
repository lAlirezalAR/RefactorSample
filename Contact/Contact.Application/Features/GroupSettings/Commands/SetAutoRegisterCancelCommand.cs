
using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Enums;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.GroupSettings.Commands
{
    public class SetAutoRegisterCancelCommand : IRequest<bool>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string AutoRegisterCancelKeyWord { get; set; }
        public string AutoRegisterCancelLineNumber { get; set; }
        public string AutoRegisterCancelMessage { get; set; }
        //TODO get the rest
        [EnumDataType(typeof(AutoRegisterStatus))]
        public AutoRegisterStatus AutoRegisterCancel
        {
            get { return (AutoRegisterStatus)AutoRegisterCancelId; }
            set { AutoRegisterCancelId = (int)value; }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        protected virtual int AutoRegisterCancelId { get; set; }

        #region Handler
        public class Handler : IRequestHandler<SetAutoRegisterCancelCommand, bool>
        {
            private readonly IGroupSettingsRepository _repository;


            public Handler(
                IGroupSettingsRepository repository
                )
            {
                _repository = repository;
            }

            public async Task<bool> Handle(SetAutoRegisterCancelCommand request, CancellationToken cancellationToken)
            {
                var groupSettings = await _repository.GetByGroupIdAsync(request.GroupId, request.UserId, cancellationToken);
                if (groupSettings == null)
                {
                    throw new AppException("تنظیمات پیدا نشد");
                }
                groupSettings.AutoRegisterCancelActivation(request.AutoRegisterCancel, request.AutoRegisterCancelMessage, request.AutoRegisterCancelKeyWord, request.AutoRegisterCancelLineNumber);
                var result = await _repository.UpdateAsync(groupSettings, cancellationToken);
                return result != null;
            }
        }
        #endregion Handler

        #region Validator
        public class SetAutoRegisterCancelCommandValidator : AbstractValidator<SetAutoRegisterCancelCommand>
        {
            public SetAutoRegisterCancelCommandValidator()
            {
                RuleFor(c => c.GroupId)
                    .NotEmpty().WithMessage("{GroupId} is required")
                    .NotNull();
                //RuleFor(c => c.AutoRegisterCancel)
                //    .IsInEnum()
                //    .NotNull().WithMessage("{AutoRegisterCancel} is required");
            }
        }
        #endregion Validator
    }
}