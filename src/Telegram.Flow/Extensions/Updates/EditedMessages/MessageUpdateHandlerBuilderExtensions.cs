using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Extensions;

public static class EditedMessageUpdateHandlerBuilderExtensions
{
    public static IEditedMessageUpdateHandlerBuilder ForText(
        this IEditedMessageUpdateHandlerBuilder builder,
        Func<ITextEditedMessageUpdateHandlerBuilder, ITextEditedMessageUpdateHandlerBuilder>? action = null)
    {
        builder.TargetMessageTypes.Add(MessageType.Text);
        ITextEditedMessageUpdateHandlerBuilder textEditedMessageUpdateHandlerBuilder = 
            new TextEditedMessageUpdateHandlerBuilder();
        if (action is not null)
            textEditedMessageUpdateHandlerBuilder = action(textEditedMessageUpdateHandlerBuilder);
        builder.TextEditedMessageUpdateHandlerBuilders.Add(textEditedMessageUpdateHandlerBuilder);
        return builder;
    }
    
    public static IEditedMessageUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this IEditedMessageUpdateHandlerBuilder builder)
    {
        return new EditedMessageUpdateHandlerBuilder<TInjected>(builder);
    }

    public static IEditedMessageUpdateHandlerBuilder WithAsyncProcessing(
        this IEditedMessageUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static IEditedMessageUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IEditedMessageUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}