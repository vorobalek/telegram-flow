using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal interface ITextEditedMessageUpdateHandler : IHandler<ITextEditedMessageUpdateHandlerContext>;

internal interface ITextEditedMessageUpdateHandler<TInjected> : ITextEditedMessageUpdateHandler;