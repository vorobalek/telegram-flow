using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Extensions;

internal static class TextEditedMessageUpdateHandlerBuilderExtensions
{
    internal static ITextFlow Build(
        this ITextBuilder builder)
    {
        return new TextFlow(
            builder.Tasks);
    }

    internal static ITextFlow Build<TInjected>(
        this ITextBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        if (builder is ITextBuilder<TInjected> injectedBuilder)
            return new TextFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.Tasks);

        return new TextFlow(
            builder.Tasks);
    }
}