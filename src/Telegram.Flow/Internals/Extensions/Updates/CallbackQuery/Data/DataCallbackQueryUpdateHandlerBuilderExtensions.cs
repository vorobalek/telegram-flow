using Microsoft.Extensions.DependencyInjection;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Extensions;

internal static class DataCallbackQueryUpdateHandlerBuilderExtensions
{
    internal static IDataCallbackQueryUpdateHandler Build(
        this IDataCallbackQueryUpdateHandlerBuilder builder)
    {
        return new DataCallbackQueryUpdateHandler(
            builder.TargetData,
            builder.TargetDataPrefixes,
            builder.ProcessingTasks);
    }
    
    internal static IDataCallbackQueryUpdateHandler Build<TInjected>(
        this IDataCallbackQueryUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        if (builder is IDataCallbackQueryUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new DataCallbackQueryUpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.TargetData,
                tInjectedBuilder.TargetDataPrefixes,
                tInjectedBuilder.ProcessingTasks);

        return builder.Build();
    }
}