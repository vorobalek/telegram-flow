namespace Telegram.Flow.Infrastructure;

public abstract class Flow<TContext>(
    IEnumerable<AsyncProcessingDelegate<TContext>> tasks) : 
    IFlow<TContext>
    where TContext : IContext
{
    public virtual async Task ProcessAsync(TContext context, CancellationToken cancellationToken)
    {
        await ProcessInternalAsync(context, cancellationToken);
    }
    
    protected virtual async Task ProcessInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        await Task.WhenAll(tasks.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}