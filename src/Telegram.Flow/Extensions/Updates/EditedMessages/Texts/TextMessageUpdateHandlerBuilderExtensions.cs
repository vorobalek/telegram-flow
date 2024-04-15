using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Extensions;

public static class TextEditedMessageUpdateHandlerBuilderExtensions
{
    public static ITextEditedMessageUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this ITextEditedMessageUpdateHandlerBuilder builder)
    {
        return new TextEditedMessageUpdateHandlerBuilder<TInjected>(builder);
    }

    public static ITextEditedMessageUpdateHandlerBuilder WithAsyncProcessing(
        this ITextEditedMessageUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static ITextEditedMessageUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this ITextEditedMessageUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}