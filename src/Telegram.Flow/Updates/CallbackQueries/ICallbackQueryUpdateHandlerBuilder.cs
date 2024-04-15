using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Updates.CallbackQueries;

public interface ICallbackQueryUpdateHandlerBuilder : IAsyncProcessingBuilder<ICallbackQueryUpdateHandlerContext>
{
    IList<IDataCallbackQueryUpdateHandlerBuilder> DataCallbackQueryUpdateHandlerBuilders { get; }
}

public interface ICallbackQueryUpdateHandlerBuilder<TInjected> :
    ICallbackQueryUpdateHandlerBuilder,
    IAsyncProcessingBuilder<ICallbackQueryUpdateHandlerContext, TInjected>;