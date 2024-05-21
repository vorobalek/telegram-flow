using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries.Data;

namespace Telegram.Flow.Updates.CallbackQueries;

public interface ICallbackQueryBuilder :
    IBuilder<ICallbackQueryContext>
{
    ICollection<IDataBuilder> DataBuilders { get; }

    public ICallbackQueryBuilder<TInjected> WithInjection<TInjected>(TInjected injected);
}

public interface ICallbackQueryBuilder<TInjected> :
    ICallbackQueryBuilder,
    IBuilder<ICallbackQueryContext, TInjected>;