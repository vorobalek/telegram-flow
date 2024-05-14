using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryBuilder : ICallbackQueryBuilder
{
    public IList<IDataBuilder> DataBuilders { get; protected init; } =
        new List<IDataBuilder>();
    
    public IList<AsyncProcessingDelegate<ICallbackQueryContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<ICallbackQueryContext>>();
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
    
    public IList<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>>();
}