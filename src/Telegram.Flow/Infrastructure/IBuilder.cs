namespace Telegram.Flow.Infrastructure;

public interface IBuilder<TContext>
{
    internal IList<AsyncProcessingDelegate<TContext>> Tasks { get; }
}

public interface IBuilder<TContext, TInjected>
{
    internal IList<AsyncProcessingDelegate<TContext, TInjected>> InjectedTasks { get; }
}