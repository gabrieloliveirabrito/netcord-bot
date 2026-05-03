using NetCord;
using NetCord.JsonModels;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace NetCordBot.Features.MakeHolder;

public class MakeHolderCommand(IHttpClientFactory httpClientFactory) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("holder", "Create a holder image")]
    public async Task<InteractionMessageProperties> Make(
        [SlashCommandParameter(Name = "width", Description = "The width of holder image")] int width,
        [SlashCommandParameter(Name = "height", Description = "The height of holder")] int? height = null
    )
    {
        var reply = new InteractionMessageProperties();
        var author = Context.Interaction.User;
        var displayName = author.GlobalName ?? author.Username;
        var avatar = author.GetAvatarUrl(ImageFormat.Png) ?? author.DefaultAvatarUrl;

        var embed = new EmbedProperties()
        .WithTitle("Make holder command")
        .WithAuthor(new EmbedAuthorProperties { Name = displayName, IconUrl = avatar?.ToString() });

        if (width is < 100 or > 300)
        {
            embed.WithTitle("Invalid width parameter").WithDescription("The width cannot be less than 100 or greather than 300.").WithColor(new Color(0xcc3300));
            return reply.AddEmbeds(embed);
        }

        if (height is not null and (< 100 or > 300))
        {
            embed.WithTitle("Invalid height parameter").WithDescription("The height cannot be less than 100 or greather than 300.").WithColor(new Color(0xcc3300));
            return reply.AddEmbeds(embed);
        }

        var size = height is null ? width.ToString() : $"{width}x{height}";

        try
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://dummyjson.com/image/{size}");

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var attachment = new AttachmentProperties("holder.png", stream);

            embed.WithTitle("Holder image generated")
            .WithDescription($"The image with size {size} has been generated successfully!")
            .WithColor(new Color(0x00FF00))
            .WithImage(new EmbedImageProperties("attachment://holder.png"));

            return reply.AddEmbeds(embed).AddAttachments(attachment);
        }
        catch (Exception ex)
        {
            embed.WithTitle("Internal error").WithDescription(ex.Message).WithColor(new Color(0xFF0000));
            return reply.AddEmbeds(embed);
        }
    }
}