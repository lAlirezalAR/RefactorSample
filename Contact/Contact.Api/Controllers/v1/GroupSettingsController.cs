using AutoMapper;
using Contact.Api.Endpoint;
using Contact.Application.Dto.RegularUser;
using Contact.Application.Features.GroupSettings.Commands;
using Contact.Application.Features.GroupSettings.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utilities.Framework;


namespace Contact.Api.Controllers.v1
{
    [Authorize]
    public class GroupSettingsController : BaseApiControllerWithDefaultRoute
    {
        private readonly IMediator _mediator;
        private readonly IClaimHelper _claimHelper;
        private readonly IMapper _mapper;
        private readonly int currentUserId;
        public GroupSettingsController(IMediator mediator, IClaimHelper claimHelper, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _claimHelper = claimHelper;
            currentUserId = _claimHelper.GetUserId();
            _mapper = mapper;
        }
        #region GET

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResult<GroupSettingsApiDto>> GetGroupSettingsById(int id, CancellationToken cancellationToken = default)
        {
            var query = new GetGroupSettingsByIdQuery() { Id = id, UserId = currentUserId };
            var groupSettings = await _mediator.Send(query, cancellationToken);
            return Ok(_mapper.Map<GroupSettingsApiDto>(groupSettings));
        }
        #endregion GET

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> SetAutoRegister([FromBody] SetAutoRegisterApiDto commandDto)
        {
            var command = _mapper.Map<SetAutoRegisterCommand>(commandDto);
            command.UserId = currentUserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> SetAutoRegisterCancel([FromBody] SetAutoRegisterCancelApiDto commandDto)
        {
            var command = _mapper.Map<SetAutoRegisterCancelCommand>(commandDto);
            command.UserId = currentUserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        //[HttpPost(Name = "CreateGroupSettings")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //public async Task<ApiResult<int>> CreateGroupSettings([FromBody] CreateGroupSettingsCommand command, CancellationToken cancellationToken = default)
        //{
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return Ok(result);
        //}
        #endregion POST

        #region PUT

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> UpdateGroupSettings([FromBody] GroupSettingsApiDto commandDto)
        {
            var command = _mapper.Map<UpdateGroupSettingsCommand>(commandDto);
            command.UserId = currentUserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion PUT

        #region DELETE

        //[HttpDelete(Name = "DeleteGroupSettings")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<ApiResult> DeleteGroupSettings([FromBody] DeleteGroupSettingsCommand command)
        //{
        //    command.CurrentUserId = currentUserId;
        //    await _mediator.Send(command);
        //    return Ok();
        //}

        #endregion DELETE
    }
}
