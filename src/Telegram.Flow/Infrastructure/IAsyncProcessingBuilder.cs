namespace Telegram.Flow.Infrastructure;

public interface IAsyncProcessingBuilder<TContext>
{
    internal IList<AsyncProcessingDelegate<TContext>> ProcessingTasks { get; }
}

public interface IAsyncProcessingBuilder<TContext, TInjected>
{
    internal IList<AsyncProcessingDelegate<TContext, TInjected>> InjectedProcessingTasks { get; }
}