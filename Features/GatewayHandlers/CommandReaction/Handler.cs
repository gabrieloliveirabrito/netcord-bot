namespace NetCordBot.Handlers.GatewayHandlers;

using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

public class CommandReactionHandler : IMessageCreateGatewayHandler
{
    public async ValueTask HandleAsync(Message message)
    {
        if(message.Type is MessageType.ApplicationCommand)
        {
            await message.AddReactionAsync(new NetCord.Rest.ReactionEmojiProperties("✅"));
        }
    }
}