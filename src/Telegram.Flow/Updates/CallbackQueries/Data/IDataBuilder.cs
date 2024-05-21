using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.CallbackQueries.Data;

public interface IDataBuilder :
    IBuilder<IDataContext>
{
    internal ISet<string> TargetData { get; }
    internal ISet<string> TargetDataPrefixes { get; }

    public IDataBuilder<TInjected> WithInjection<TInjected>(TInjected injected);
}

public interface IDataBuilder<TInjected> :
    IDataBuilder,
    IBuilder<IDataContext, TInjected>;