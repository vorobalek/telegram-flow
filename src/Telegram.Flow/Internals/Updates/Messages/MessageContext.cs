using Telegram.Bot.Types;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates.Messages;

internal record MessageContext(
    Update Update, 
    Message Message) : IMessageContext;