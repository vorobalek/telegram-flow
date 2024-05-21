using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Extensions;

public static class BotCommandTextMessageUpdateHandlerBuilderExtensions
{
    public static TBuilder ForExact<TBuilder>(
        this TBuilder builder,
        string exactBotCommand)
        where TBuilder : IBotCommandBuilder
    {
        builder.TargetCommands.Add(exactBotCommand);
        return builder;
    }

    public static TBuilder ForPrefix<TBuilder>(
        this TBuilder builder,
        string botCommandPrefix)
        where TBuilder : IBotCommandBuilder
    {
        builder.TargetCommandPrefixes.Add(botCommandPrefix);
        return builder;
    }

    public static TBuilder AllowInline<TBuilder>(
        this TBuilder builder,
        bool allowInline = true)
        where TBuilder : IBotCommandBuilder
    {
        builder.AllowInline = allowInline;
        return builder;
    }

    public static IBotCommandBuilder WithAsyncProcessing(
        this IBotCommandBuilder builder,
        AsyncProcessingDelegate<IBotCommandContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }

    public static IBotCommandBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IBotCommandBuilder<TInjected> builder,
        AsyncProcessingDelegate<IBotCommandContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}