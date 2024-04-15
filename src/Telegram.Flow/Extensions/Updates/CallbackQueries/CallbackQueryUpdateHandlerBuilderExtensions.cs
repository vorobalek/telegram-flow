using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Extensions;

public static class CallbackQueryUpdateHandlerBuilderExtensions
{
    public static ICallbackQueryUpdateHandlerBuilder ForData(
        this ICallbackQueryUpdateHandlerBuilder builder,
        Func<IDataCallbackQueryUpdateHandlerBuilder, IDataCallbackQueryUpdateHandlerBuilder>? action = null)
    {
        IDataCallbackQueryUpdateHandlerBuilder dataCallbackQueryUpdateHandlerBuilder = 
            new DataCallbackQueryUpdateHandlerBuilder();
        if (action is not null)
            dataCallbackQueryUpdateHandlerBuilder = action(dataCallbackQueryUpdateHandlerBuilder);
        builder.DataCallbackQueryUpdateHandlerBuilders.Add(dataCallbackQueryUpdateHandlerBuilder);
        return builder;
    }
    
    public static ICallbackQueryUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this ICallbackQueryUpdateHandlerBuilder builder)
    {
        return new CallbackQueryUpdateHandlerBuilder<TInjected>(builder);
    }

    public static ICallbackQueryUpdateHandlerBuilder WithAsyncProcessing(
        this ICallbackQueryUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static ICallbackQueryUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this ICallbackQueryUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}