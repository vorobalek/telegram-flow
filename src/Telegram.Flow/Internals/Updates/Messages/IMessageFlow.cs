using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates.Messages;

internal interface IMessageFlow : IFlow<IMessageContext>;

internal interface IMessageFlow<TInjected> : IMessageFlow;