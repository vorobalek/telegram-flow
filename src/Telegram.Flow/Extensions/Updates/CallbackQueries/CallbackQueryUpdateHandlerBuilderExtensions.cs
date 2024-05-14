using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Extensions;

public static class CallbackQueryUpdateHandlerBuilderExtensions
{
    public static ICallbackQueryBuilder ForData(
        this ICallbackQueryBuilder builder,
        Func<IDataBuilder, IDataBuilder>? action = null)
    {
        IDataBuilder dataBuilder = 
            new DataBuilder();
        if (action is not null)
            dataBuilder = action(dataBuilder);
        builder.DataBuilders.Add(dataBuilder);
        return builder;
    }
    
    public static ICallbackQueryBuilder<TInjected> WithInjection<TInjected>(
        this ICallbackQueryBuilder builder)
    {
        return new CallbackQueryBuilder<TInjected>(builder);
    }

    public static ICallbackQueryBuilder WithAsyncProcessing(
        this ICallbackQueryBuilder builder,
        AsyncProcessingDelegate<ICallbackQueryContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static ICallbackQueryBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this ICallbackQueryBuilder<TInjected> builder,
        AsyncProcessingDelegate<ICallbackQueryContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}