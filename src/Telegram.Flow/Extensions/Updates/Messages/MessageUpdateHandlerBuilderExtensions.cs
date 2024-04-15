using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Extensions;

public static class MessageUpdateHandlerBuilderExtensions
{
    public static IMessageUpdateHandlerBuilder ForText(
        this IMessageUpdateHandlerBuilder builder,
        Func<ITextMessageUpdateHandlerBuilder, ITextMessageUpdateHandlerBuilder>? action = null)
    {
        builder.TargetMessageTypes.Add(MessageType.Text);
        ITextMessageUpdateHandlerBuilder textMessageUpdateHandlerBuilder = 
            new TextMessageUpdateHandlerBuilder();
        if (action is not null)
            textMessageUpdateHandlerBuilder = action(textMessageUpdateHandlerBuilder);
        builder.TextMessageUpdateHandlerBuilders.Add(textMessageUpdateHandlerBuilder);
        return builder;
    }
    
    public static IMessageUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this IMessageUpdateHandlerBuilder builder)
    {
        return new MessageUpdateHandlerBuilder<TInjected>(builder);
    }

    public static IMessageUpdateHandlerBuilder WithAsyncProcessing(
        this IMessageUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<IMessageUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static IMessageUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IMessageUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<IMessageUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}