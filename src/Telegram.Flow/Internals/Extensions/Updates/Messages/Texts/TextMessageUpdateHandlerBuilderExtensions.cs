using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Extensions;

internal static class TextMessageUpdateHandlerBuilderExtensions
{
    internal static ITextFlow Build(
        this ITextBuilder builder)
    {
        var botCommandFlows = 
            builder.BotCommandBuilders.Select(botCommandBuilder => 
                botCommandBuilder.Build());

        return new TextFlow(
            botCommandFlows,
            builder.Tasks);
    }

    internal static ITextFlow Build<TInjected>(
        this ITextBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var botCommandTextMessageFlows = 
            builder.BotCommandBuilders.Select(botCommandBuilder => 
                botCommandBuilder.Build<TInjected>(serviceProvider));

        if (builder is ITextBuilder<TInjected> injectedBuilder)
            return new TextFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.Tasks,
                botCommandTextMessageFlows);

        return new TextFlow(
            botCommandTextMessageFlows,
            builder.Tasks);
    }
}