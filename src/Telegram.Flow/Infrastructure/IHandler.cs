namespace Telegram.Flow.Infrastructure;

public interface IHandler<in TContext>
{
    Task ProcessAsync(TContext context, CancellationToken cancellationToken);
}