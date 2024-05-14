using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryFlow(
    IEnumerable<IDataFlow> dataFlows,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryContext>> tasks) :
    Flow<ICallbackQueryContext>(tasks),
    ICallbackQueryFlow
{
    public override async Task ProcessAsync(ICallbackQueryContext context, CancellationToken cancellationToken)
    {
        if (context.CallbackQuery.Data is { } callbackQueryData)
            await Task.WhenAll(dataFlows.Select(dataFlow =>
                dataFlow.ProcessAsync(
                    new DataContext(
                        context.Update,
                        context.CallbackQuery,
                        callbackQueryData),
                    cancellationToken)));
        
        await base.ProcessAsync(context, cancellationToken);
    }
}

internal class CallbackQueryFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>> injectedTasks,
    IEnumerable<IDataFlow> dataCallbackQueryFlows,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryContext>> tasks) :
    CallbackQueryFlow(
        dataCallbackQueryFlows,
        tasks),
    ICallbackQueryFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(ICallbackQueryContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(processingDelegate =>
            processingDelegate.Invoke(context, injected, cancellationToken)));
    }
}