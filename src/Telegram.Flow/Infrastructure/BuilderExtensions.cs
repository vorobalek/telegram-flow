namespace Telegram.Flow.Infrastructure;

internal static class BuilderExtensions
{
    internal static TBuilder WithAsyncProcessingInternal<TBuilder, TContext>(
        this TBuilder builder,
        AsyncProcessingDelegate<TContext> func)
        where TBuilder : IBuilder<TContext>
        where TContext : IContext
    {
        builder.Tasks.Add(func);
        return builder;
    }

    internal static TBuilder WithAsyncProcessingInternal<TBuilder, TContext, TInjected>(
        this TBuilder builder,
        AsyncProcessingDelegate<TContext, TInjected> func)
        where TBuilder : IBuilder<TContext, TInjected>
        where TContext : IContext
    {
        builder.InjectedTasks.Add(func);
        return builder;
    }
}