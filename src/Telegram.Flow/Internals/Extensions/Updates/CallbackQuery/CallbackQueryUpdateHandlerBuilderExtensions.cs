using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Extensions;

internal static class CallbackQueryUpdateHandlerBuilderExtensions
{
    internal static ICallbackQueryFlow Build(
        this ICallbackQueryBuilder builder)
    {
        var dataFlows =
            builder.DataBuilders.Select(dataBuilder =>
                dataBuilder.Build());

        return new CallbackQueryFlow(
            dataFlows,
            builder.Tasks);
    }

    internal static ICallbackQueryFlow Build<TInjected>(
        this ICallbackQueryBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var dataFlows =
            builder.DataBuilders.Select(dataBuilder =>
                dataBuilder.Build<TInjected>(serviceProvider));

        if (builder is ICallbackQueryBuilder<TInjected> injectedBuilder)
            return new CallbackQueryFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                dataFlows,
                injectedBuilder.Tasks);

        return new CallbackQueryFlow(
            dataFlows,
            builder.Tasks);
    }
}