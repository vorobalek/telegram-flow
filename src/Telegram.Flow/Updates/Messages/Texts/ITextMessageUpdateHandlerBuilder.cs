using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Updates.Messages.Texts;

public interface ITextMessageUpdateHandlerBuilder : IAsyncProcessingBuilder<ITextMessageUpdateHandlerContext>
{
    internal IList<IBotCommandTextMessageUpdateHandlerBuilder> BotCommandTextMessageUpdateHandlerBuilders { get; }
}

public interface ITextMessageUpdateHandlerBuilder<TInjected> :
    ITextMessageUpdateHandlerBuilder,
    IAsyncProcessingBuilder<ITextMessageUpdateHandlerContext, TInjected>;