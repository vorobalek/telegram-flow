using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Extensions;

public static class BotCommandTextMessageUpdateHandlerBuilderExtensions
{
    public static IBotCommandTextMessageUpdateHandlerBuilder ForExact(
        this IBotCommandTextMessageUpdateHandlerBuilder builder,
        string exactBotCommand)
    {
        builder.TargetCommands.Add(exactBotCommand);
        return builder;
    }

    public static IBotCommandTextMessageUpdateHandlerBuilder ForPrefix(
        this IBotCommandTextMessageUpdateHandlerBuilder builder,
        string botCommandPrefix)
    {
        builder.TargetCommandPrefixes.Add(botCommandPrefix);
        return builder;
    }

    public static IBotCommandTextMessageUpdateHandlerBuilder AllowInline(
        this IBotCommandTextMessageUpdateHandlerBuilder builder,
        bool allowInline = true)
    {
        builder.AllowInline = allowInline;
        return builder;
    }
    
    public static IBotCommandTextMessageUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this IBotCommandTextMessageUpdateHandlerBuilder builder)
    {
        return new BotCommandTextMessageUpdateHandlerBuilder<TInjected>(builder);
    }

    public static IBotCommandTextMessageUpdateHandlerBuilder WithAsyncProcessing(
        this IBotCommandTextMessageUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static IBotCommandTextMessageUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IBotCommandTextMessageUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}