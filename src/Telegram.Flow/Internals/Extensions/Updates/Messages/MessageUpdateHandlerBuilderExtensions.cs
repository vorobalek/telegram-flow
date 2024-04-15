using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Extensions;

internal static class MessageUpdateHandlerBuilderExtensions
{
    internal static IMessageUpdateHandler Build(
        this IMessageUpdateHandlerBuilder builder)
    {
        var textMessageUpdateHandlerBuilders =
            builder.TextMessageUpdateHandlerBuilders.Select(textMessageUpdateHandlerBuilder =>
                textMessageUpdateHandlerBuilder.Build());

        return new MessageUpdateHandler(
            builder.TargetMessageTypes,
            builder.ProcessingTasks,
            textMessageUpdateHandlerBuilders);
    }

    internal static IMessageUpdateHandler Build<TInjected>(
        this IMessageUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var textMessageUpdateHandlerBuilders =
            builder.TextMessageUpdateHandlerBuilders.Select(textMessageUpdateHandlerBuilder =>
                textMessageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        if (builder is IMessageUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new MessageUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.TargetMessageTypes,
                tInjectedBuilder.ProcessingTasks,
                textMessageUpdateHandlerBuilders);

        return new MessageUpdateHandler(
            builder.TargetMessageTypes,
            builder.ProcessingTasks,
            textMessageUpdateHandlerBuilders);
    }
}