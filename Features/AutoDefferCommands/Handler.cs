namespace NetCordBot.Handlers;

using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

public class AutoDefferCommands : IInteractionCreateGatewayHandler
{
    public async ValueTask HandleAsync(Interaction interaction)
    {
        await interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Loading));
    }
}