using NetCord;
using NetCord.Hosting.Gateway;

namespace NetCordBot.Features.Math;

public class MathComponentHandler(ILogger<MathComponentHandler> logger) : IInteractionCreateGatewayHandler
{
    public ValueTask HandleAsync(Interaction interaction)
    {
        if (interaction is ComponentInteraction component)
        {
            logger.LogInformation(component.Data.CustomId);
        }

        return ValueTask.CompletedTask;
    }
}