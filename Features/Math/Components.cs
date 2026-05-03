using NetCord;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;

namespace NetCordBot.Features.Math.Components;

public class MathComponents : ComponentInteractionModule<ButtonInteractionContext>
{
    private async Task SendResult(double a, double b, double result, string description)
    {
        var embed = Context.Interaction.Message.Embeds.FirstOrDefault();

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredModifyMessage);
        await Context.Interaction.ModifyResponseAsync(x =>
        {
            if (embed is not null)
            {
                x.WithEmbeds([
                   new EmbedProperties
                   {
                       Title = embed.Title,
                       Description = description,
                       Footer = new EmbedFooterProperties
                       {
                           Text = $"A = {a} | B = {b}"
                       }
                   } 
                ]);
            }
            else
            {
                x.Content = description;
            }

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

            x.AddComponents(new ActionRowProperties
            {
                new ButtonProperties($"set-math-a:{result}:{b}", "Set A value from result", ButtonStyle.Primary),
                new ButtonProperties($"set-math-b:{a}:{result}", "Set B value from result", ButtonStyle.Primary)
            });
        });
    }

    [ComponentInteraction("sum")]
    public async Task Sum(double a, double b)
    {
        var result = a + b;
        var description = $"{a} + {b}\n\nResult: {result}";

        await SendResult(a, b, result, description);
    }

    [ComponentInteraction("subtract")]
    public async Task Sbutract(double a, double b)
    {
        var result = a - b;
        var description = $"{a} - {b}\n\nResult: {result}";

        await SendResult(a, b, result, description);
    }

    [ComponentInteraction("multiply")]
    public async Task Multiply(double a, double b)
    {
        var result = a * b;
        var description = $"{a} * {b}\n\nResult: {result}";

        await SendResult(a, b, result, description);
    }

    [ComponentInteraction("divide")]
    public async Task Divide(double a, double b)
    {
        var result = b is 0 ? double.NaN : a / b;
        var description = $"{a} / {b}\n\nResult: {result}";

        await SendResult(a, b, result, description);
    }

    [ComponentInteraction("set-math-a")]
    public async Task SetMathA(double b, double result)
    {
        var description = $"Changed A to {result}";

        await SendResult(result, b, result, description);
    }

    [ComponentInteraction("set-math-b")]
    public async Task SetMathB(double a, double result)
    {
        var description = $"Changed B to {result}";

        await SendResult(a, result, result, description);
    }

    [ComponentInteraction("root")]
    public async Task Root(double a, double b)
    {
        var result = b is 0 ? double.NaN : System.Math.Pow(a, 1d / b);
        var description = $"{a}^(1/{b})\n\nResult: {result}";

        await SendResult(a, b, result, description);
    }

    [ComponentInteraction("pow")]
    public async Task Pow(double a, double b)
    {
        var result = a is 0 ? double.NaN : System.Math.Pow(a, b);
        var description = $"{a} ^ {b}\n\nResult: {result}";

        await SendResult(a, b, result, description);
    }
}