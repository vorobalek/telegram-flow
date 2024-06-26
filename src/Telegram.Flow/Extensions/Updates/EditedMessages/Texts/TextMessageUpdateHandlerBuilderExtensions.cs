using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Extensions;

public static class TextEditedMessageUpdateHandlerBuilderExtensions
{
    public static ITextBuilder WithAsyncProcessing(
        this ITextBuilder builder,
        AsyncProcessingDelegate<ITextContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }

    public static ITextBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this ITextBuilder<TInjected> builder,
        AsyncProcessingDelegate<ITextContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}