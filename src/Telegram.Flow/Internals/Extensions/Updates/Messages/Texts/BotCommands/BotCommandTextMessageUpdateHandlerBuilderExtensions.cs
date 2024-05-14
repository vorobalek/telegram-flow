using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Extensions;

internal static class BotCommandTextMessageUpdateHandlerBuilderExtensions
{
    internal static IBotCommandFlow Build(
        this IBotCommandBuilder builder)
    {
        return new BotCommandFlow(
            builder.AllowInline,
            builder.TargetCommands,
            builder.TargetCommandPrefixes,
            builder.Tasks);
    }
    
    internal static IBotCommandFlow Build<TInjected>(
        this IBotCommandBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        if (builder is IBotCommandBuilder<TInjected> injectedBuilder)
            return new BotCommandFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.AllowInline,
                injectedBuilder.TargetCommands,
                injectedBuilder.TargetCommandPrefixes,
                injectedBuilder.Tasks);

        return builder.Build();
    }
}