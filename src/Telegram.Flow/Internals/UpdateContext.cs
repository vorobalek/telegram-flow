using Telegram.Bot.Types;
using Telegram.Flow.Updates;

namespace Telegram.Flow.Internals;

internal record UpdateContext(Update Update) : IUpdateContext;