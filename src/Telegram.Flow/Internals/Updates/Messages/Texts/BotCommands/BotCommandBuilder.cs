using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal class BotCommandBuilder : IBotCommandBuilder
{
    public bool AllowInline { get; set; }
    public ISet<string> TargetCommands { get; protected init; } = new SortedSet<string>();
    public ISet<string> TargetCommandPrefixes { get; protected init; } = new SortedSet<string>();

    public IList<AsyncProcessingDelegate<IBotCommandContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IBotCommandContext>>();
}

internal class BotCommandBuilder<TInjected> :
    BotCommandBuilder,
    IBotCommandBuilder<TInjected>
{
    public BotCommandBuilder(IBotCommandBuilder prototypeBuilder)
    {
        AllowInline = prototypeBuilder.AllowInline;
        TargetCommands = prototypeBuilder.TargetCommands;
        TargetCommandPrefixes = prototypeBuilder.TargetCommandPrefixes;
        Tasks = prototypeBuilder.Tasks;
    }
    
    public IList<AsyncProcessingDelegate<IBotCommandContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IBotCommandContext, TInjected>>();
}