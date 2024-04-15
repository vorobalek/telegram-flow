using Telegram.Bot.Types;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal record TextEditedMessageUpdateHandlerContext(
    Update Update, 
    Message EditedMessage, 
    string Text) : ITextEditedMessageUpdateHandlerContext;