using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal class DataBuilder : IDataBuilder
{
    public ISet<string> TargetData { get; protected init; } = new SortedSet<string>();

    public ISet<string> TargetDataPrefixes { get; protected init; } = new SortedSet<string>();

    public IList<AsyncProcessingDelegate<IDataContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IDataContext>>();
}

internal class DataBuilder<TInjected> :
    DataBuilder,
    IDataBuilder<TInjected>
{
    public DataBuilder(IDataBuilder prototypeBuilder)
    {
        TargetData = prototypeBuilder.TargetData;
        TargetDataPrefixes = prototypeBuilder.TargetDataPrefixes;
        Tasks = prototypeBuilder.Tasks;
    }
    
    public IList<AsyncProcessingDelegate<IDataContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IDataContext, TInjected>>();
}