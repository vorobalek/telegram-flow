using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryBuilder :
    Builder<ICallbackQueryContext>,
    ICallbackQueryBuilder
{
    public ICollection<IDataBuilder> DataBuilders { get; protected init; } =
        new List<IDataBuilder>();

    public ICallbackQueryBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new CallbackQueryBuilder<TInjected>(this, injected);
    }

    public override IFlow<ICallbackQueryContext> Build()
    {
        var dataFlows = BuildDependencies();

        return new CallbackQueryFlow(
            dataFlows,
            Tasks);
    }

    protected IEnumerable<IFlow<IDataContext>> BuildDependencies()
    {
        return DataBuilders.Select(builder => builder.Build());
    }
}

internal sealed class CallbackQueryBuilder<TInjected> :
    CallbackQueryBuilder,
    ICallbackQueryBuilder<TInjected>
{
    public CallbackQueryBuilder(ICallbackQueryBuilder prototypeBuilder, TInjected injected)
    {
        DataBuilders = prototypeBuilder.DataBuilders;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ICallbackQueryContext, TInjected>>();

    public override IFlow<ICallbackQueryContext> Build()
    {
        var dataFlows = BuildDependencies();

        return new CallbackQueryFlow<TInjected>(
            Injected,
            InjectedTasks,
            dataFlows,
            Tasks);
    }
}