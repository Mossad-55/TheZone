using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheZone.Application.Features.Chats.Queries.GetUserChats;
using TheZone.Application.Features.Users.Commands.CreateUser;

namespace TheZone.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _sender.Send(
            command,
            cancellationToken
        );

        if(result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Value },
            result.Value);
    }

    [HttpGet("{userId:guid}/chats")]
    public async Task<IActionResult> GetChats(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserChatsQuery(userId);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}