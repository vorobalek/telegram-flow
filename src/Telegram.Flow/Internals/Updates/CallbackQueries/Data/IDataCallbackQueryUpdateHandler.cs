using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal interface IDataCallbackQueryUpdateHandler : IHandler<IDataCallbackQueryUpdateHandlerContext>;

internal interface IDataCallbackQueryUpdateHandler<TInjected> : IDataCallbackQueryUpdateHandler;