using Telegram.Bot.Types;
using Telegram.Flow.Updates;

namespace Telegram.Flow.Internals.Updates;

internal record UpdateContext(Update Update) : IUpdateContext;