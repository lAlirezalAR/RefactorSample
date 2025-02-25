using Contact.Api.Endpoint;
using Contact.Application.Dto;
using Contact.Application.Features.GroupSettings.Commands;
using Contact.Application.Features.GroupSettings.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utilities.Framework;


namespace Contact.Api.Controllers.v1.Admin
{
    [Authorize]
    public class GroupSettingsController : BaseApiControllerWithDefaultRouteAdmin
    {
        private readonly IMediator _mediator;
        public GroupSettingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #region GET

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResult<GroupSettingsDto>> GetGroupSettingsById(int id, int userId, CancellationToken cancellationToken = default)
        {
            var query = new GetGroupSettingsByIdQuery() { Id = id, UserId = userId };
            var groupSettings = await _mediator.Send(query, cancellationToken);
            return Ok(groupSettings);
        }
        #endregion GET

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> SetAutoRegister([FromBody] SetAutoRegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> SetAutoRegisterCancel([FromBody] SetAutoRegisterCancelCommand command)
        {
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
        public async Task<ApiResult<bool>> UpdateGroupSettings([FromBody] UpdateGroupSettingsCommand command)
        {
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
