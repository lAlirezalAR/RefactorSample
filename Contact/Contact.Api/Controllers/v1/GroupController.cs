using AutoMapper;
using Contact.Api.Endpoint;
using Contact.Application.Dto.RegularUser;
using Contact.Application.Features.Groups.Commands;
using Contact.Application.Features.Groups.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utilities.Framework;


namespace Contact.Api.Controllers.v1
{

    [Authorize]
    public class GroupController : BaseApiControllerWithDefaultRoute
    {
        private readonly IMediator _mediator;
        private readonly IClaimHelper _claimHelper;
        private readonly int currentUserId;
        private readonly IMapper _mapper;
        public GroupController(
            IMediator mediator
            , IClaimHelper claimHelper,
            IMapper mapper
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _claimHelper = claimHelper;
            currentUserId = _claimHelper.GetUserId();
            _mapper = mapper;
        }
        #region GET

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResult<IEnumerable<GroupApiDto>>> GetAllGroups(CancellationToken cancellationToken = default)
        {
            var query = new GetAllGroupsQuery() { UserId = currentUserId };
            var allGroups = await _mediator.Send(query, cancellationToken);
            return Ok(_mapper.Map<List<GroupApiDto>>(allGroups));
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResult<GroupApiDto>> GetGroupById(int id, CancellationToken cancellationToken = default)
        {
            var query = new GetGroupByIdQuery()
            {
                Id = id,
                UserId = currentUserId
            };
            var group = await _mediator.Send(query, cancellationToken);
            return Ok(_mapper.Map<GroupApiDto>(group));
        }

        #endregion GET

        #region POST
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ApiResult<GroupApiDto>> CreateGroup([FromBody] CreateGroupApiDto commandDto, CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<CreateGroupCommand>(commandDto);
            command.UserId = currentUserId;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<GroupApiDto>(result));
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> UpdateGroup([FromBody] GroupApiDto commandDto, CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<UpdateGroupCommand>(commandDto);
            command.UserId = currentUserId;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<bool>> DeleteGroup(int Id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteGroupCommand() { Id = Id, UserId = currentUserId };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion DELETE
    }
}
