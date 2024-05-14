using Telegram.Bot.Types;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal record EditedMessageContext(
    Update Update, 
    Message EditedMessage) : IEditedMessageContext;