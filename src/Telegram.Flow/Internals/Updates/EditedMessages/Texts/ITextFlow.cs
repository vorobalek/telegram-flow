using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal interface ITextFlow : IFlow<ITextContext>;

internal interface ITextFlow<TInjected> : ITextFlow;