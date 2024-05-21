using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Extensions;

public static class DataCallbackQueryUpdateHandlerBuilderExtensions
{
    public static TBuilder ForExact<TBuilder>(
        this TBuilder builder,
        string exactBotCommand)
        where TBuilder : IDataBuilder
    {
        builder.TargetData.Add(exactBotCommand);
        return builder;
    }

    public static TBuilder ForPrefix<TBuilder>(
        this TBuilder builder,
        string botCommandPrefix)
        where TBuilder : IDataBuilder
    {
        builder.TargetDataPrefixes.Add(botCommandPrefix);
        return builder;
    }

    public static IDataBuilder WithAsyncProcessing(
        this IDataBuilder builder,
        AsyncProcessingDelegate<IDataContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }

    public static IDataBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IDataBuilder<TInjected> builder,
        AsyncProcessingDelegate<IDataContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}