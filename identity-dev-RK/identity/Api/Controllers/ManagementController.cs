using Api.Extensions;
using Application.Management.Commands;
using Application.Management.Queries;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ManagementController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ManagementController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("assignAdminRole{id}")]
        [Authorize(Policy = Policy.SuperAdmin)]
        public async Task<IActionResult> AssignAdminRole([FromRoute] Guid id)
        {
            var command = new AssignAdminRoleCommand(id);
            await _commandDispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPost("removeAdminRole{id}")]
        [Authorize(Policy = Policy.SuperAdmin)]
        public async Task<IActionResult> RemoveAdminRole([FromRoute] Guid id)
        {
            var command = new RemoveAdminRoleCommand(id);
            await _commandDispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> GetListOfSepecyficRoleType([FromQuery]Role roles)
        {
            var query = new GetListOfSpecyficRoleTypeQuery(roles);
            var users = await _queryDispatcher.QueryAsync(query);
            return Ok(users);
        }
    }
}
