namespace Telegram.Flow.Updates.CallbackQueries.Data;

public interface IDataCallbackQueryUpdateHandlerContext : ICallbackQueryUpdateHandlerContext
{
    string Data { get; }
}