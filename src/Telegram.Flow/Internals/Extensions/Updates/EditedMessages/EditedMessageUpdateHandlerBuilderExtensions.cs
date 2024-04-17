using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Extensions;

internal static class EditedMessageUpdateHandlerBuilderExtensions
{
    internal static IEditedMessageUpdateHandler Build(
        this IEditedMessageUpdateHandlerBuilder builder)
    {
        var textEditedMessageUpdateHandlerBuilders =
            builder.TextEditedMessageUpdateHandlerBuilders.Select(textEditedMessageUpdateHandlerBuilder =>
                textEditedMessageUpdateHandlerBuilder.Build());

        return new EditedMessageUpdateHandler(
            builder.TargetEditedMessageTypes,
            builder.ProcessingTasks,
            textEditedMessageUpdateHandlerBuilders);
    }

    internal static IEditedMessageUpdateHandler Build<TInjected>(
        this IEditedMessageUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var textEditedMessageUpdateHandlerBuilders =
            builder.TextEditedMessageUpdateHandlerBuilders.Select(textEditedMessageUpdateHandlerBuilder =>
                textEditedMessageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        if (builder is IEditedMessageUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new EditedMessageUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.TargetEditedMessageTypes,
                tInjectedBuilder.ProcessingTasks,
                textEditedMessageUpdateHandlerBuilders);

        return new EditedMessageUpdateHandler(
            builder.TargetEditedMessageTypes,
            builder.ProcessingTasks,
            textEditedMessageUpdateHandlerBuilders);
    }
}