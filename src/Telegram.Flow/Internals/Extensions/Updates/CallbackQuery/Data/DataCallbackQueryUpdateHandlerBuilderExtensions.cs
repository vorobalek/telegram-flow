using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Extensions;

internal static class DataCallbackQueryUpdateHandlerBuilderExtensions
{
    internal static IDataFlow Build(
        this IDataBuilder builder)
    {
        return new DataFlow(
            builder.TargetData,
            builder.TargetDataPrefixes,
            builder.Tasks);
    }
    
    internal static IDataFlow Build<TInjected>(
        this IDataBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        if (builder is IDataBuilder<TInjected> injectedBuilder)
            return new DataFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.TargetData,
                injectedBuilder.TargetDataPrefixes,
                injectedBuilder.Tasks);

        return builder.Build();
    }
}