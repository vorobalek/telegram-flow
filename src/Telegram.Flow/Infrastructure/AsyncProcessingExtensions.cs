namespace Telegram.Flow.Infrastructure;

internal static class AsyncProcessingExtensions
{
    internal static TAsyncProcessingBuilder WithAsyncProcessingInternal<TAsyncProcessingBuilder, TContext>(
        this TAsyncProcessingBuilder builder,
        AsyncProcessingDelegate<TContext> func)
        where TAsyncProcessingBuilder : IAsyncProcessingBuilder<TContext>
    {
        builder.ProcessingTasks.Add(func);
        return builder;
    }

    internal static TAsyncProcessingBuilder WithAsyncProcessingInternal<TAsyncProcessingBuilder, TContext, TInjected>(
        this TAsyncProcessingBuilder builder,
        AsyncProcessingDelegate<TContext, TInjected> func)
        where TAsyncProcessingBuilder : IAsyncProcessingBuilder<TContext, TInjected>
    {
        builder.InjectedProcessingTasks.Add(func);
        return builder;
    }
}