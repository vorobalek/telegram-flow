using Telegram.Bot.Types;

namespace Telegram.Flow.Updates.CallbackQueries;

public interface ICallbackQueryUpdateHandlerContext : IUpdateHandlerContext
{
    CallbackQuery CallbackQuery { get; }
}