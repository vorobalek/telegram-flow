using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Internals.Updates.CallbackQueries;

internal class CallbackQueryUpdateHandlerBuilder : ICallbackQueryUpdateHandlerBuilder
{
    public IList<IDataCallbackQueryUpdateHandlerBuilder> DataCallbackQueryUpdateHandlerBuilders { get; protected init; } =
        new List<IDataCallbackQueryUpdateHandlerBuilder>();
    
    public IList<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext>>();
}

internal class CallbackQueryUpdateHandlerBuilder<TInjected> :
    CallbackQueryUpdateHandlerBuilder,
    ICallbackQueryUpdateHandlerBuilder<TInjected>
{
    public CallbackQueryUpdateHandlerBuilder(ICallbackQueryUpdateHandlerBuilder prototypeBuilder)
    {
        DataCallbackQueryUpdateHandlerBuilders = prototypeBuilder.DataCallbackQueryUpdateHandlerBuilders;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<ICallbackQueryUpdateHandlerContext, TInjected>>();
}