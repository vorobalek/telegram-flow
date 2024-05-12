using Telegram.Bot.Types;
using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates;

public interface IUpdateHandler : IHandler<Update>
{
    string? DisplayName { get; }
}

public interface IUpdateHandler<TInjected> : IUpdateHandler;