using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal class DataBuilder : Builder<IDataContext>, IDataBuilder
{
    public ISet<string> TargetData { get; protected init; } = new SortedSet<string>();

    public ISet<string> TargetDataPrefixes { get; protected init; } = new SortedSet<string>();
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
    
    public ICollection<AsyncProcessingDelegate<IDataContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IDataContext, TInjected>>();
}