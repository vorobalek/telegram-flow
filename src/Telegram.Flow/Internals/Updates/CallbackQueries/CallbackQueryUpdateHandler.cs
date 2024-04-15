using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryUpdateHandler(
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext>> processingDelegates,
    IEnumerable<IDataCallbackQueryUpdateHandler> dataCallbackQueryUpdateHandlers)
    : ICallbackQueryUpdateHandler
{
    public async Task ProcessAsync(ICallbackQueryUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        if (context.CallbackQuery.Data is { } callbackQueryData)
            await Task.WhenAll(dataCallbackQueryUpdateHandlers.Select(dataCallbackQueryUpdateHandler =>
                dataCallbackQueryUpdateHandler.ProcessAsync(
                    new DataCallbackQueryUpdateHandlerContext(
                        context.Update,
                        context.CallbackQuery,
                        callbackQueryData),
                    cancellationToken)));
        
        await ProcessInternalAsync(context, cancellationToken);
    }

    protected virtual Task ProcessInternalAsync(ICallbackQueryUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class CallbackQueryUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext>> processingDelegates,
    IEnumerable<IDataCallbackQueryUpdateHandler> dataCallbackQueryUpdateHandlers) :
    CallbackQueryUpdateHandler(
        processingDelegates,
        dataCallbackQueryUpdateHandlers),
    ICallbackQueryUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(ICallbackQueryUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}