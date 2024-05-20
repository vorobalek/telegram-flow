using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal class DataFlow(
    ISet<string> targetData,
    ISet<string> targetDataPrefixes,
    IEnumerable<AsyncProcessingDelegate<IDataContext>> tasks) :
    Flow<IDataContext>(tasks),
    IDataFlow
{
    public override async Task ProcessAsync(IDataContext context, CancellationToken cancellationToken)
    {
        if (targetData.Contains(context.Data) ||
            targetDataPrefixes.Any(targetPrefix => context.Data.StartsWith(targetPrefix)))
        {
            await base.ProcessAsync(context, cancellationToken);
        }
    }
}

internal class DataFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IDataContext, TInjected>> injectedTasks,
    ISet<string> targetData,
    ISet<string> targetDataPrefixes,
    IEnumerable<AsyncProcessingDelegate<IDataContext>> tasks) :
    DataFlow(
        targetData,
        targetDataPrefixes,
        tasks),
    IDataFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(IDataContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}