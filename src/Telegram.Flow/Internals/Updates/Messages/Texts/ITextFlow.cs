using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal interface ITextFlow : IFlow<ITextContext>;

internal interface ITextFlow<TInjected> : ITextFlow;