using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal interface IEditedMessageUpdateHandler : IHandler<IEditedMessageUpdateHandlerContext>;

internal interface IEditedMessageUpdateHandler<TInjected> : IEditedMessageUpdateHandler;