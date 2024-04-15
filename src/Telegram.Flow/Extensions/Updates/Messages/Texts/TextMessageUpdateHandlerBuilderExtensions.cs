using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Extensions;

public static class TextMessageUpdateHandlerBuilderExtensions
{
    public static ITextMessageUpdateHandlerBuilder ForBotCommand(
        this ITextMessageUpdateHandlerBuilder builder,
        Func<IBotCommandTextMessageUpdateHandlerBuilder, IBotCommandTextMessageUpdateHandlerBuilder>? action = null)
    {
        IBotCommandTextMessageUpdateHandlerBuilder botCommandTextMessageUpdateHandlerBuilder = 
            new BotCommandTextMessageUpdateHandlerBuilder();
        if (action is not null)
            botCommandTextMessageUpdateHandlerBuilder = action(botCommandTextMessageUpdateHandlerBuilder);
        builder.BotCommandTextMessageUpdateHandlerBuilders.Add(botCommandTextMessageUpdateHandlerBuilder);
        return builder;
    }
    
    public static ITextMessageUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this ITextMessageUpdateHandlerBuilder builder)
    {
        return new TextMessageUpdateHandlerBuilder<TInjected>(builder);
    }

    public static ITextMessageUpdateHandlerBuilder WithAsyncProcessing(
        this ITextMessageUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<ITextMessageUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static ITextMessageUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this ITextMessageUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<ITextMessageUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}