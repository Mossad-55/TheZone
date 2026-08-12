using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheZone.Application.Features.Chats.Commands.CreatePrivateChat;

namespace TheZone.Api.Controllers;

[ApiController]
[Route("api/chats")]
public sealed class ChatsController : ControllerBase
{
    private readonly ISender _sender;

    public ChatsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("private")]
    public async Task<IActionResult> CreatePrivate(
        CreatePrivateChatCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken
        );

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(CreatePrivate),
            new { id = result.Value },
            result.Value);
    }
}