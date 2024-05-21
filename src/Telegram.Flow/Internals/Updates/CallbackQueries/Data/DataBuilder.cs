using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries.Data;

internal class DataBuilder :
    Builder<IDataContext>,
    IDataBuilder
{
    public ISet<string> TargetData { get; protected init; } = new SortedSet<string>();

    public ISet<string> TargetDataPrefixes { get; protected init; } = new SortedSet<string>();

    public IDataBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new DataBuilder<TInjected>(this, injected);
    }

    public override IFlow<IDataContext> Build()
    {
        return new DataFlow(
            TargetData,
            TargetDataPrefixes,
            Tasks);
    }
}

internal sealed class DataBuilder<TInjected> :
    DataBuilder,
    IDataBuilder<TInjected>
{
    public DataBuilder(IDataBuilder prototypeBuilder, TInjected injected)
    {
        TargetData = prototypeBuilder.TargetData;
        TargetDataPrefixes = prototypeBuilder.TargetDataPrefixes;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<IDataContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IDataContext, TInjected>>();

    public override IFlow<IDataContext> Build()
    {
        return new DataFlow<TInjected>(
            Injected,
            InjectedTasks,
            TargetData,
            TargetDataPrefixes,
            Tasks);
    }
}