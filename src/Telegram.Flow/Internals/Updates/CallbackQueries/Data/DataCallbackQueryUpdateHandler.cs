using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal class DataCallbackQueryUpdateHandler(
    ISet<string> targetData,
    ISet<string> targetDataPrefixes,
    IEnumerable<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext>> processingDelegates)
    : IDataCallbackQueryUpdateHandler
{
    public async Task ProcessAsync(IDataCallbackQueryUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        if (targetData.Contains(context.Data) ||
            targetDataPrefixes.Any(targetPrefix => context.Data.StartsWith(targetPrefix)))
        {
            await Task.WhenAll(processingDelegates.Select(processingDelegate =>
                processingDelegate.Invoke(context, cancellationToken)));

            await ProcessInternalAsync(context, cancellationToken);
        }
    }

    protected virtual Task ProcessInternalAsync(IDataCallbackQueryUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class DataCallbackQueryUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    ISet<string> targetData,
    ISet<string> targetDataPrefixes,
    IEnumerable<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext>> processingDelegates) :
    DataCallbackQueryUpdateHandler(
        targetData,
        targetDataPrefixes,
        processingDelegates),
    IDataCallbackQueryUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(IDataCallbackQueryUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}