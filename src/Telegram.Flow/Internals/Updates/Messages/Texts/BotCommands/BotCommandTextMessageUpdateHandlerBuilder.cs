using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal class BotCommandTextMessageUpdateHandlerBuilder : IBotCommandTextMessageUpdateHandlerBuilder
{
    public bool AllowInline { get; set; }
    public ISet<string> TargetCommands { get; protected init; } = new SortedSet<string>();
    public ISet<string> TargetCommandPrefixes { get; protected init; } = new SortedSet<string>();

    public IList<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext>>();
}

internal class BotCommandTextMessageUpdateHandlerBuilder<TInjected> :
    BotCommandTextMessageUpdateHandlerBuilder,
    IBotCommandTextMessageUpdateHandlerBuilder<TInjected>
{
    public BotCommandTextMessageUpdateHandlerBuilder(IBotCommandTextMessageUpdateHandlerBuilder prototypeBuilder)
    {
        AllowInline = prototypeBuilder.AllowInline;
        TargetCommands = prototypeBuilder.TargetCommands;
        TargetCommandPrefixes = prototypeBuilder.TargetCommandPrefixes;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext, TInjected>>();
}