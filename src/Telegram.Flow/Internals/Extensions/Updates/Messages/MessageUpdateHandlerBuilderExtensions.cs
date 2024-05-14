using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Extensions;

internal static class MessageUpdateHandlerBuilderExtensions
{
    internal static IMessageFlow Build(
        this IMessageBuilder builder)
    {
        var textBuilders =
            builder.TextBuilders.Select(textMessageUpdateHandlerBuilder =>
                textMessageUpdateHandlerBuilder.Build());

        return new MessageFlow(
            builder.TargetMessageTypes,
            textBuilders,
            builder.Tasks);
    }

    internal static IMessageFlow Build<TInjected>(
        this IMessageBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var textBuilders =
            builder.TextBuilders.Select(textMessageUpdateHandlerBuilder =>
                textMessageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        if (builder is IMessageBuilder<TInjected> injectedBuilder)
            return new MessageFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.TargetMessageTypes,
                textBuilders,
                injectedBuilder.Tasks);

        return new MessageFlow(
            builder.TargetMessageTypes,
            textBuilders,
            builder.Tasks);
    }
}