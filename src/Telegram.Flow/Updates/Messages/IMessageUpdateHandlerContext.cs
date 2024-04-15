using Telegram.Bot.Types;

namespace Telegram.Flow.Updates.Messages;

public interface IMessageUpdateHandlerContext : IUpdateHandlerContext
{
    Message Message { get; }
}