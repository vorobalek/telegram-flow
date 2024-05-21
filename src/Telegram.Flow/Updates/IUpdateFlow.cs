using Telegram.Bot.Types;
using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates;

public interface IUpdateFlow : IFlow<IUpdateContext>
{
    public string? DisplayName { get; }

    Task ProcessAsync(Update update, CancellationToken cancellationToken);
}