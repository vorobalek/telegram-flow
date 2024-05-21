using Telegram.Bot.Types;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal record DataContext(
    Update Update,
    CallbackQuery CallbackQuery,
    string Data) : IDataContext;