using Telegram.Flow.Internals.Updates;
using Telegram.Flow.Updates;

namespace Telegram.Flow;

public static class TelegramFlow
{
    public static IUpdateBuilder New => new UpdateBuilder();
}