using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Extensions;

internal static class TextMessageUpdateHandlerBuilderExtensions
{
    internal static ITextMessageUpdateHandler Build(
        this ITextMessageUpdateHandlerBuilder builder)
    {
        var botCommandTextMessageUpdateHandlers = 
            builder.BotCommandTextMessageUpdateHandlerBuilders.Select(botCommandTextMessageUpdateHandlerBuilder => 
                botCommandTextMessageUpdateHandlerBuilder.Build());

        return new TextMessageUpdateHandler(
            builder.ProcessingTasks,
            botCommandTextMessageUpdateHandlers);
    }

    internal static ITextMessageUpdateHandler Build<TInjected>(
        this ITextMessageUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var botCommandTextMessageUpdateHandlers = 
            builder.BotCommandTextMessageUpdateHandlerBuilders.Select(botCommandTextMessageUpdateHandlerBuilder => 
                botCommandTextMessageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        if (builder is ITextMessageUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new TextMessageUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.ProcessingTasks,
                botCommandTextMessageUpdateHandlers);

        return new TextMessageUpdateHandler(
            builder.ProcessingTasks,
            botCommandTextMessageUpdateHandlers);
    }
}