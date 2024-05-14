using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Updates.CallbackQueries;

public interface ICallbackQueryBuilder : IBuilder<ICallbackQueryContext>
{
    IList<IDataBuilder> DataBuilders { get; }
}

public interface ICallbackQueryBuilder<TInjected> :
    ICallbackQueryBuilder,
    IBuilder<ICallbackQueryContext, TInjected>;