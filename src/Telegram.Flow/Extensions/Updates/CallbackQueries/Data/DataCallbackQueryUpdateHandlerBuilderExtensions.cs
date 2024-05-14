using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Extensions;

public static class DataCallbackQueryUpdateHandlerBuilderExtensions
{
    public static IDataBuilder ForExact(
        this IDataBuilder builder,
        string exactBotCommand)
    {
        builder.TargetData.Add(exactBotCommand);
        return builder;
    }

    public static IDataBuilder ForPrefix(
        this IDataBuilder builder,
        string botCommandPrefix)
    {
        builder.TargetDataPrefixes.Add(botCommandPrefix);
        return builder;
    }
    
    public static IDataBuilder<TInjected> WithInjection<TInjected>(
        this IDataBuilder builder)
    {
        return new DataBuilder<TInjected>(builder);
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