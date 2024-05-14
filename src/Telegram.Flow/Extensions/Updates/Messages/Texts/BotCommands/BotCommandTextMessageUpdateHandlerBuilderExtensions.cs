using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Extensions;

public static class BotCommandTextMessageUpdateHandlerBuilderExtensions
{
    public static IBotCommandBuilder ForExact(
        this IBotCommandBuilder builder,
        string exactBotCommand)
    {
        builder.TargetCommands.Add(exactBotCommand);
        return builder;
    }

    public static IBotCommandBuilder ForPrefix(
        this IBotCommandBuilder builder,
        string botCommandPrefix)
    {
        builder.TargetCommandPrefixes.Add(botCommandPrefix);
        return builder;
    }

    public static IBotCommandBuilder AllowInline(
        this IBotCommandBuilder builder,
        bool allowInline = true)
    {
        builder.AllowInline = allowInline;
        return builder;
    }
    
    public static IBotCommandBuilder<TInjected> WithInjection<TInjected>(
        this IBotCommandBuilder builder)
    {
        return new BotCommandBuilder<TInjected>(builder);
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