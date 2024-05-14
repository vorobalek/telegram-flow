using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Extensions;

public static class MessageUpdateHandlerBuilderExtensions
{
    public static IMessageBuilder ForText(
        this IMessageBuilder builder,
        Func<ITextBuilder, ITextBuilder>? action = null)
    {
        builder.TargetMessageTypes.Add(MessageType.Text);
        ITextBuilder textBuilder = 
            new TextBuilder();
        if (action is not null)
            textBuilder = action(textBuilder);
        builder.TextBuilders.Add(textBuilder);
        return builder;
    }
    
    public static IMessageBuilder<TInjected> WithInjection<TInjected>(
        this IMessageBuilder builder)
    {
        return new MessageBuilder<TInjected>(builder);
    }

    public static IMessageBuilder WithAsyncProcessing(
        this IMessageBuilder builder,
        AsyncProcessingDelegate<IMessageContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static IMessageBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IMessageBuilder<TInjected> builder,
        AsyncProcessingDelegate<IMessageContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}