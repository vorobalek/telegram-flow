using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Extensions;

internal static class BotCommandTextMessageUpdateHandlerBuilderExtensions
{
    internal static IBotCommandTextMessageUpdateHandler Build(
        this IBotCommandTextMessageUpdateHandlerBuilder builder)
    {
        return new BotCommandTextMessageUpdateHandler(
            builder.AllowInline,
            builder.TargetCommands,
            builder.TargetCommandPrefixes,
            builder.ProcessingTasks);
    }
    
    internal static IBotCommandTextMessageUpdateHandler Build<TInjected>(
        this IBotCommandTextMessageUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        if (builder is IBotCommandTextMessageUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new BotCommandTextMessageUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.AllowInline,
                tInjectedBuilder.TargetCommands,
                tInjectedBuilder.TargetCommandPrefixes,
                tInjectedBuilder.ProcessingTasks);

        return builder.Build();
    }
}