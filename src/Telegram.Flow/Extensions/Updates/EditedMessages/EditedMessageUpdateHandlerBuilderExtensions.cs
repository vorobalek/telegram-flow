using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Extensions;

public static class EditedMessageUpdateHandlerBuilderExtensions
{
    public static TBuilder ForText<TBuilder>(
        this TBuilder builder,
        Func<ITextBuilder, ITextBuilder>? action = null)
        where TBuilder : IEditedMessageBuilder
    {
        builder.TargetTypes.Add(MessageType.Text);
        ITextBuilder textBuilder =
            new TextBuilder();
        if (action is not null)
            textBuilder = action(textBuilder);
        builder.TextBuilders.Add(textBuilder);
        return builder;
    }

    public static IEditedMessageBuilder WithAsyncProcessing(
        this IEditedMessageBuilder builder,
        AsyncProcessingDelegate<IEditedMessageContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }

    public static IEditedMessageBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IEditedMessageBuilder<TInjected> builder,
        AsyncProcessingDelegate<IEditedMessageContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}