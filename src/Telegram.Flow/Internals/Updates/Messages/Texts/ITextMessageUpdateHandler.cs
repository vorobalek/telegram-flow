using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal interface ITextMessageUpdateHandler : IHandler<ITextMessageUpdateHandlerContext>;

internal interface ITextMessageUpdateHandler<TInjected> : ITextMessageUpdateHandler;