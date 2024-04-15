using Telegram.Bot.Types;

namespace Telegram.Flow.Updates;

public interface IUpdateHandlerContext
{
    Update Update { get; }
}