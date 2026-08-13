using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheZone.Application.Features.Messages.Commands.SendMessage;

namespace TheZone.Api.Controllers;

[ApiController]
[Route("api/messages")]
public sealed class MessagesController : ControllerBase
{
    private readonly ISender _sender;
    public MessagesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Send(
        SendMessageCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(Send),
            new { id = result.Value },
            result.Value);
    }
}