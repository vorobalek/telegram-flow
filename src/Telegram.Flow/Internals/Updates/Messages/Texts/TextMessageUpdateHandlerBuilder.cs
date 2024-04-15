using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal class TextMessageUpdateHandlerBuilder : ITextMessageUpdateHandlerBuilder
{
    public IList<IBotCommandTextMessageUpdateHandlerBuilder> BotCommandTextMessageUpdateHandlerBuilders { get; protected init; } =
        new List<IBotCommandTextMessageUpdateHandlerBuilder>();

    public IList<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext>>();
}

internal class TextMessageUpdateHandlerBuilder<TInjected> :
    TextMessageUpdateHandlerBuilder,
    ITextMessageUpdateHandlerBuilder<TInjected>
{
    public TextMessageUpdateHandlerBuilder(ITextMessageUpdateHandlerBuilder prototypeBuilder)
    {
        BotCommandTextMessageUpdateHandlerBuilders = prototypeBuilder.BotCommandTextMessageUpdateHandlerBuilders;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext, TInjected>>();
}