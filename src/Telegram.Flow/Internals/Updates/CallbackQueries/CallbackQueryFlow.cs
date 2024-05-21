using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries.Data;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryFlow(
    IEnumerable<IFlow<IDataContext>> dataFlows,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryContext>> tasks) :
    Flow<ICallbackQueryContext>(tasks)
{
    public override async Task ProcessAsync(ICallbackQueryContext context, CancellationToken cancellationToken)
    {
        if (context.CallbackQuery.Data is { } callbackQueryData)
            await Task.WhenAll(dataFlows.Select(flow =>
                flow.ProcessAsync(
                    new DataContext(
                        context.Update,
                        context.CallbackQuery,
                        callbackQueryData),
                    cancellationToken)));

        await base.ProcessAsync(context, cancellationToken);
    }
}

internal sealed class CallbackQueryFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>> injectedTasks,
    IEnumerable<IFlow<IDataContext>> dataCallbackQueryFlows,
    IEnumerable<AsyncProcessingDelegate<ICallbackQueryContext>> tasks) :
    CallbackQueryFlow(
        dataCallbackQueryFlows,
        tasks)
{
    protected override async Task ProcessInternalAsync(ICallbackQueryContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}