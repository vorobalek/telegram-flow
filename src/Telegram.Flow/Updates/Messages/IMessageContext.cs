using Telegram.Bot.Types;

namespace Telegram.Flow.Updates.Messages;

public interface IMessageContext : IUpdateContext
{
    Message Message { get; }
}