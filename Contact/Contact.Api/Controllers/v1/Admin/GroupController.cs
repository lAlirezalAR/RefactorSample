using Contact.Api.Endpoint;
using Contact.Application.Dto;
using Contact.Application.Features.Groups.Commands;
using Contact.Application.Features.Groups.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utilities.Framework;

namespace Contact.Api.Controllers.v1.Admin
{

    [Authorize("AdminRole")]
    public class GroupController : BaseApiControllerWithDefaultRouteAdmin
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #region GET

        [Route("[action]/{userId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResult<IEnumerable<GroupDto>>> GetAllGroups(int userId, CancellationToken cancellationToken = default)
        {
            var query = new GetAllGroupsQuery() { UserId = userId };
            var allGroups = await _mediator.Send(query, cancellationToken);
            return Ok(allGroups);
        }

        [Route("[action]/{id}/{userId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResult<GroupDto>> GetGroupById(int id, int userId, CancellationToken cancellationToken = default)
        {
            var query = new GetGroupByIdQuery()
            {
                Id = id,
                UserId = userId
            };
            var group = await _mediator.Send(query, cancellationToken);
            return Ok(group);
        }

        #endregion GET

        #region POST
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ApiResult<GroupDto>> CreateGroup([FromBody] CreateGroupCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> UpdateGroup([FromBody] UpdateGroupCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> DeleteGroup([FromBody] DeleteGroupCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion DELETE
    }
}
