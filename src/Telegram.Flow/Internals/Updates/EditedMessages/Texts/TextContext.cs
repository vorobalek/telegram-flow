using Telegram.Bot.Types;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal record TextContext(
    Update Update, 
    Message EditedMessage, 
    string Text) : ITextContext;