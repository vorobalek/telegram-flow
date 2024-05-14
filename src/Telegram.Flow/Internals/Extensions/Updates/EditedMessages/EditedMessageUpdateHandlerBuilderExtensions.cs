using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Extensions;

internal static class EditedMessageUpdateHandlerBuilderExtensions
{
    internal static IEditedMessageFlow Build(
        this IEditedMessageBuilder builder)
    {
        var textFlows =
            builder.TextBuilders.Select(textBuilder =>
                textBuilder.Build());

        return new EditedMessageFlow(
            builder.TargetTypes,
            textFlows,
            builder.Tasks);
    }

    internal static IEditedMessageFlow Build<TInjected>(
        this IEditedMessageBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var textFlows =
            builder.TextBuilders.Select(textBuilder =>
                textBuilder.Build<TInjected>(serviceProvider));

        if (builder is IEditedMessageBuilder<TInjected> injectedBuilder)
            return new EditedMessageFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.TargetTypes,
                injectedBuilder.Tasks,
                textFlows);

        return new EditedMessageFlow(
            builder.TargetTypes,
            textFlows,
            builder.Tasks);
    }
}