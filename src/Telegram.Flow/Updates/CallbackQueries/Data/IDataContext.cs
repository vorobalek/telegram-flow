namespace Telegram.Flow.Updates.CallbackQueries.Data;

public interface IDataContext : ICallbackQueryContext
{
    string Data { get; }
}