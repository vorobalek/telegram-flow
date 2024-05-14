using Telegram.Bot.Types;

namespace Telegram.Flow.Updates.CallbackQueries;

public interface ICallbackQueryContext : IUpdateContext
{
    CallbackQuery CallbackQuery { get; }
}