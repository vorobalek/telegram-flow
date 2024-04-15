using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal interface ICallbackQueryUpdateHandler : IHandler<ICallbackQueryUpdateHandlerContext>;

internal interface ICallbackQueryUpdateHandler<TInjected> : ICallbackQueryUpdateHandler;