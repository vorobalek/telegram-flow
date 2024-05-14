using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates;

public interface IUpdateFlow : IFlow<IUpdateContext>
{
    string? DisplayName { get; }
}

public interface IUpdateFlow<TInjected> : IUpdateFlow;