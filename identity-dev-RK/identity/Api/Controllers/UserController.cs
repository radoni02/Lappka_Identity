using Api.Requests;
using Application.User.Commands;
using Application.User.Queries;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : BaseController
{
    private readonly ICommandDispatcher _command;
    private readonly IQueryDispatcher _query;

    public UserController(ICommandDispatcher command, IQueryDispatcher query)
    {
        _command = command;
        _query = query;
    }

    [HttpGet("GetUser/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetOneUser([FromRoute] Guid id)
    {
        var query = new GetOneUserQuery()
        {
            UserId = id
        };
        var userData = await _query.QueryAsync(query);
        return Ok(userData);
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUserData()
    {
        var query = new GetCurrentUserDataQuery()
        {
            UserId = GetCurrentUserId()
        };
        var userData = await _query.QueryAsync(query);
        return Ok(userData);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataRequest request)
    {
        var command = new UpdateUserDataCommand(GetCurrentUserId(), request.FirstName, request.LastName, request.ProfilePicture);
        await _command.SendAsync(command);
        return NoContent();
    }


    [HttpPatch("Password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordRequest request)
    {
        var command = new UpdateUserPasswordCommand(GetCurrentUserId(), request.Password);
        await _command.SendAsync(command);
        return NoContent();
    }

    [HttpPatch("Email")]
    public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailRequest request)
    {
        var command = new UpdateUserEmailCommand(GetCurrentUserId(), request.AddressEmail);
        await _command.SendAsync(command);
        return NoContent();
    }

    [HttpPut("ConfirmUpdatedEmail/{token}")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmUpdateUserEmail([FromRoute] string token)
    {
        var command = new ConfirmUpdateUserEmailCommand(token);
        await _command.SendAsync(command);
        return NoContent();
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser()
    {
        var command = new DeleteUserCommand(GetCurrentUserId());
        await _command.SendAsync(command);
        return NoContent();
    }

}
