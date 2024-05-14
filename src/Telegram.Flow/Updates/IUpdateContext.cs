using Telegram.Bot.Types;
using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates;

public interface IUpdateContext : IContext
{
    Update Update { get; }
}