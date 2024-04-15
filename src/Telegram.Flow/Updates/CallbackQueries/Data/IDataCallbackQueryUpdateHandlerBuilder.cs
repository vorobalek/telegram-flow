using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.CallbackQueries.Data;

public interface IDataCallbackQueryUpdateHandlerBuilder : 
    IAsyncProcessingBuilder<IDataCallbackQueryUpdateHandlerContext>
{
    internal ISet<string> TargetData { get; }
    internal ISet<string> TargetDataPrefixes { get; }
}

public interface IDataCallbackQueryUpdateHandlerBuilder<TInjected> : 
    IDataCallbackQueryUpdateHandlerBuilder, 
    IAsyncProcessingBuilder<IDataCallbackQueryUpdateHandlerContext, TInjected>;