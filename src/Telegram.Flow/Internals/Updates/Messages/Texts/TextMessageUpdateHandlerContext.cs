using Telegram.Bot.Types;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal record TextMessageUpdateHandlerContext(
    Update Update, 
    Message Message, 
    string Text) : ITextMessageUpdateHandlerContext;