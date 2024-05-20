namespace Telegram.Flow.Infrastructure;

public interface IBuilder<TContext>
    where TContext : IContext
{
    internal ICollection<AsyncProcessingDelegate<TContext>> Tasks { get; }
}

public interface IBuilder<TContext, TInjected>
    where TContext : IContext
{
    internal ICollection<AsyncProcessingDelegate<TContext, TInjected>> InjectedTasks { get; }
}