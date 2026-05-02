namespace NetCordBot.Features.Message;

public class SendMessageDTO
{
    public ulong ChannelID { get; set; }
    public string? Content { get; set; }
}