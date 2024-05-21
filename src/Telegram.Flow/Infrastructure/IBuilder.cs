namespace Telegram.Flow.Infrastructure;

public interface IBuilder<TContext>
    where TContext : IContext
{
    internal ICollection<AsyncProcessingDelegate<TContext>> Tasks { get; }
    public IFlow<TContext> Build();
}

public interface IBuilder<TContext, TInjected> :
    IBuilder<TContext>
    where TContext : IContext
{
    internal TInjected Injected { get; }
    internal ICollection<AsyncProcessingDelegate<TContext, TInjected>> InjectedTasks { get; }
}