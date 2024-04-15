using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Extensions;

internal static class CallbackQueryUpdateHandlerBuilderExtensions
{
    internal static ICallbackQueryUpdateHandler Build(
        this ICallbackQueryUpdateHandlerBuilder builder)
    {
        var dataCallbackQueryUpdateHandlers =
            builder.DataCallbackQueryUpdateHandlerBuilders.Select(dataCallbackQueryUpdateHandlerBuilder =>
                dataCallbackQueryUpdateHandlerBuilder.Build());

        return new CallbackQueryUpdateHandler(
            builder.ProcessingTasks,
            dataCallbackQueryUpdateHandlers);
    }

    internal static ICallbackQueryUpdateHandler Build<TInjected>(
        this ICallbackQueryUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var dataCallbackQueryUpdateHandlers =
            builder.DataCallbackQueryUpdateHandlerBuilders.Select(dataCallbackQueryUpdateHandlerBuilder =>
                dataCallbackQueryUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        if (builder is ICallbackQueryUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new CallbackQueryUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.ProcessingTasks,
                dataCallbackQueryUpdateHandlers);

        return new CallbackQueryUpdateHandler(
            builder.ProcessingTasks,
            dataCallbackQueryUpdateHandlers);
    }
}