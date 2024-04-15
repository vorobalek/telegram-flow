using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal class DataCallbackQueryUpdateHandlerBuilder : IDataCallbackQueryUpdateHandlerBuilder
{
    public ISet<string> TargetData { get; protected init; } = new SortedSet<string>();

    public ISet<string> TargetDataPrefixes { get; protected init; } = new SortedSet<string>();

    public IList<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext>>();
}

internal class DataCallbackQueryUpdateHandlerBuilder<TInjected> :
    DataCallbackQueryUpdateHandlerBuilder,
    IDataCallbackQueryUpdateHandlerBuilder<TInjected>
{
    public DataCallbackQueryUpdateHandlerBuilder(IDataCallbackQueryUpdateHandlerBuilder prototypeBuilder)
    {
        TargetData = prototypeBuilder.TargetData;
        TargetDataPrefixes = prototypeBuilder.TargetDataPrefixes;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<IDataCallbackQueryUpdateHandlerContext, TInjected>>();
}