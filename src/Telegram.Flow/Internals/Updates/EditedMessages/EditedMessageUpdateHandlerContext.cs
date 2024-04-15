using Telegram.Bot.Types;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal record EditedMessageUpdateHandlerContext(
    Update Update, 
    Message EditedMessage) : IEditedMessageUpdateHandlerContext;