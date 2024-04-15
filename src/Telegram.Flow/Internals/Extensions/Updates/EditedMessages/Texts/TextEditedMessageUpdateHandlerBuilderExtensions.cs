using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Extensions;

internal static class TextEditedMessageUpdateHandlerBuilderExtensions
{
    internal static ITextEditedMessageUpdateHandler Build(
        this ITextEditedMessageUpdateHandlerBuilder builder)
    {
        return new TextEditedMessageUpdateHandler(
            builder.ProcessingTasks);
    }

    internal static ITextEditedMessageUpdateHandler Build<TInjected>(
        this ITextEditedMessageUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        if (builder is ITextEditedMessageUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new TextEditedMessageUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.ProcessingTasks);

        return new TextEditedMessageUpdateHandler(
            builder.ProcessingTasks);
    }
}