namespace Telegram.Flow.Infrastructure.Internals;

internal abstract class Builder<TContext> :
    IBuilder<TContext>
    where TContext : IContext
{
    public ICollection<AsyncProcessingDelegate<TContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<TContext>>();
}