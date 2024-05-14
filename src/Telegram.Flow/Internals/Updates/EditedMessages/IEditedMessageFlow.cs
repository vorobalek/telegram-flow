using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal interface IEditedMessageFlow : IFlow<IEditedMessageContext>;

internal interface IEditedMessageFlow<TInjected> : IEditedMessageFlow;