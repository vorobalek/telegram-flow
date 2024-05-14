using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal interface IDataFlow : IFlow<IDataContext>;

internal interface IDataFlow<TInjected> : IDataFlow;