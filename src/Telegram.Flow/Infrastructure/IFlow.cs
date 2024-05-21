namespace Telegram.Flow.Infrastructure;

public interface IFlow<in TContext>
    where TContext : IContext
{
    Task ProcessAsync(TContext context, CancellationToken cancellationToken);
}