using Telegram.Bot.Types;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal record CallbackQueryUpdateHandlerContext(
    Update Update, 
    CallbackQuery CallbackQuery) : ICallbackQueryUpdateHandlerContext;