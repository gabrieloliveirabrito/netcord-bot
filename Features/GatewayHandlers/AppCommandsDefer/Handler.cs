using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace NetCordBot.Features.GatewayHandlers;

public class AppCommandsDefer : IInteractionCreateGatewayHandler
{
    public async ValueTask HandleAsync(Interaction interaction)
    {
        if (interaction is ApplicationCommandInteraction)
        {
            await interaction.SendResponseAsync(
                InteractionCallback.DeferredMessage(MessageFlags.Loading)
            );
        }
    }
}