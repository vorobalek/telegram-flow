using Telegram.Bot.Types;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates.Messages;

internal record MessageUpdateHandlerContext(
    Update Update, 
    Message Message) : IMessageUpdateHandlerContext;