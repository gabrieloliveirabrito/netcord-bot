using Microsoft.AspNetCore.Mvc;
using NetCord.Rest;

namespace NetCordBot.Features.Message;

[ApiController]
[Route("api/[controller]")]
public class MessageController(RestClient discord) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Content))
        {
            return BadRequest("The action needs the message content!");
        }

        if (dto.ChannelID <= 0)
        {
            return BadRequest("Invalid channel id!");
        }

        await discord.SendMessageAsync(dto.ChannelID, dto.Content);
        return Ok();
    }
}