using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal interface ICallbackQueryFlow : IFlow<ICallbackQueryContext>;

internal interface ICallbackQueryFlow<TInjected> : ICallbackQueryFlow;