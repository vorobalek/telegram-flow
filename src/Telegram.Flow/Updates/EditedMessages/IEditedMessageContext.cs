using Telegram.Bot.Types;

namespace Telegram.Flow.Updates.EditedMessages;

public interface IEditedMessageContext : IUpdateContext
{
    Message EditedMessage { get; }
}