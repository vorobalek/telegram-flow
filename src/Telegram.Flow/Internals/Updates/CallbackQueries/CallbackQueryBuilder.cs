using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryBuilder : Builder<ICallbackQueryContext>, ICallbackQueryBuilder
{
    public ICollection<IDataBuilder> DataBuilders { get; protected init; } =
        new List<IDataBuilder>();
}

internal class CallbackQueryBuilder<TInjected> :
    CallbackQueryBuilder,
    ICallbackQueryBuilder<TInjected>
{
    public CallbackQueryBuilder(ICallbackQueryBuilder prototypeBuilder)
    {
        DataBuilders = prototypeBuilder.DataBuilders;
        Tasks = prototypeBuilder.Tasks;
    }
    
    public ICollection<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>>();
}