using Telegram.Bot.Types;
using Telegram.Flow.Updates;

namespace Telegram.Flow.Internals.Updates;

internal record UpdateHandlerContext(Update Update) : IUpdateHandlerContext;