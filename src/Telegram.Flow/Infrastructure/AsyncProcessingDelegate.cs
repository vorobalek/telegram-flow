namespace Telegram.Flow.Infrastructure;

public delegate Task AsyncProcessingDelegate<in TContext>(
    TContext context, 
    CancellationToken cancellationToken);

public delegate Task AsyncProcessingDelegate<in TContext, in TInjected>(
    TContext context,
    TInjected injected,
    CancellationToken cancellationToken);