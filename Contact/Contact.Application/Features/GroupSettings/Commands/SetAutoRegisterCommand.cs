using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Enums;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Utilities.Framework.Exceptions;

namespace Contact.Application.Features.GroupSettings.Commands
{
    public class SetAutoRegisterCommand : IRequest<bool>
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string AutoRegisterKeyWord { get; set; }
        public string AutoRegisterLineNumber { get; set; }
        public string AutoRegisterMessage { get; set; }

        //public int GroupId { get; private set; }
        [EnumDataType(typeof(AutoRegisterStatus))]
        public AutoRegisterStatus AutoRegister
        {
            get { return (AutoRegisterStatus)AutoRegisterId; }
            set { AutoRegisterId = (int)value; }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        protected virtual int AutoRegisterId { get; set; }

        #region Handler
        public class Handler : IRequestHandler<SetAutoRegisterCommand, bool>
        {
            //private readonly IMapper _mapper;
            private readonly IGroupSettingsRepository _repository;

            public Handler(
                //IMapper mapper, 
                IGroupSettingsRepository repository
                )
            {
                //_mapper = mapper;
                _repository = repository;
            }

            public async Task<bool> Handle(SetAutoRegisterCommand request, CancellationToken cancellationToken)
            {
                var groupSettings = await _repository.GetByGroupIdAsync(request.GroupId, request.UserId, cancellationToken);
                if (groupSettings == null)
                {
                    throw new AppException("تنظیمات پیدا نشد");
                }
                groupSettings.AutoRegisterActivation(request.AutoRegister, request.AutoRegisterMessage, request.AutoRegisterKeyWord, request.AutoRegisterLineNumber);
                var result = await _repository.UpdateAsync(groupSettings, cancellationToken);
                return result != null;
            }
        }
        #endregion

        #region Validator
        public class SetAutoRegisterCommandValidator : AbstractValidator<SetAutoRegisterCommand>
        {
            public SetAutoRegisterCommandValidator()
            {
                RuleFor(c => c.GroupId)
                    .NotEmpty().WithMessage("{GroupId} is required")
                    .NotNull();
                //RuleFor(c => c.AutoRegister)
                //    .IsInEnum()
                //    .NotNull().WithMessage("{AutoRegister} is required");
            }
        }
        #endregion Validator
    }
}
