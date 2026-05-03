namespace NetCordBot.Handlers;

using NetCord.Gateway;
using NetCord.Hosting.Gateway;

public class MessageCreateHandler(ILogger<MessageCreateHandler> logger) : IMessageCreateGatewayHandler
{
    public ValueTask HandleAsync(Message message)
    {
        if (!message.Author.IsBot && !string.IsNullOrEmpty(message.Content))
        {
            logger.LogInformation($"User {message.Author.GlobalName ?? message.Author.Username} says: {message.Content}");
        }

        return ValueTask.CompletedTask;
    }
}