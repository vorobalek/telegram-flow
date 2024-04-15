using Telegram.Bot.Types;

namespace Telegram.Flow.Updates.EditedMessages;

public interface IEditedMessageUpdateHandlerContext : IUpdateHandlerContext
{
    Message EditedMessage { get; }
}