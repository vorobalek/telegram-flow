using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Extensions;

public static class DataCallbackQueryUpdateHandlerBuilderExtensions
{
    public static IDataCallbackQueryUpdateHandlerBuilder ForExact(
        this IDataCallbackQueryUpdateHandlerBuilder builder,
        string exactBotCommand)
    {
        builder.TargetData.Add(exactBotCommand);
        return builder;
    }

    public static IDataCallbackQueryUpdateHandlerBuilder ForPrefix(
        this IDataCallbackQueryUpdateHandlerBuilder builder,
        string botCommandPrefix)
    {
        builder.TargetDataPrefixes.Add(botCommandPrefix);
        return builder;
    }
    
    public static IDataCallbackQueryUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this IDataCallbackQueryUpdateHandlerBuilder builder)
    {
        return new DataCallbackQueryUpdateHandlerBuilder<TInjected>(builder);
    }

    public static IDataCallbackQueryUpdateHandlerBuilder WithAsyncProcessing(
        this IDataCallbackQueryUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static IDataCallbackQueryUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IDataCallbackQueryUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}