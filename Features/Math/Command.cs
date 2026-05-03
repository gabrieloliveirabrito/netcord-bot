using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace NetCordBot.Features.Math;

public class MathCommand : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("math", "A math command that initiate a simple calc")]
    public async Task InitiateCalc(
        [SlashCommandParameter(Name = "a", Description = "The first/left value")] double a,
        [SlashCommandParameter(Name = "b", Description = "The second/right value")] double b)
    {
        await Context.Interaction.ModifyResponseAsync(x =>
        {
            var embed = new EmbedProperties
            {
                Title = "Calculator",
                Description = $"A = {a}\nB = {b}\n\nResultado: ?",
                Footer = new EmbedFooterProperties
                {
                    Text = $"A = {a} | B = {b}"
                }
            };

            x.AddEmbeds(embed);
            x.AddComponents(new ActionRowProperties
            {
                new ButtonProperties($"sum:{a}:{b}", "Sum +", ButtonStyle.Primary),
                new ButtonProperties($"subtract:{a}:{b}", "Subtract -", ButtonStyle.Secondary),
                new ButtonProperties($"multiply:{a}:{b}", "Multiply *", ButtonStyle.Success),
                new ButtonProperties($"divide:{a}:{b}", "Divide /", ButtonStyle.Danger)
            });

            x.AddComponents(new ActionRowProperties
            {
                new ButtonProperties($"root:{a}:{b}", "Root (a ^ (1 / b))", ButtonStyle.Primary),
                new ButtonProperties($"pow:{a}:{b}", "Pow (a ^ b)", ButtonStyle.Primary),
            });
        });
    }
}