using Telegram.Bot.Types;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal record TextContext(
    Update Update,
    Message Message,
    string Text) :
    ITextContext;