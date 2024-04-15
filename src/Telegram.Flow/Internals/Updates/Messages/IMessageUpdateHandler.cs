using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates.Messages;

internal interface IMessageUpdateHandler : IHandler<IMessageUpdateHandlerContext>;

internal interface IMessageUpdateHandler<TInjected> : IMessageUpdateHandler;