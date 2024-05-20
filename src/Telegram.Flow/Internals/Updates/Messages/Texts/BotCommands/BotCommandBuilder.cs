using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal class BotCommandBuilder : Builder<IBotCommandContext>, IBotCommandBuilder
{
    public bool AllowInline { get; set; }
    public ISet<string> TargetCommands { get; protected init; } = new SortedSet<string>();
    public ISet<string> TargetCommandPrefixes { get; protected init; } = new SortedSet<string>();
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
    
    public ICollection<AsyncProcessingDelegate<IBotCommandContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IBotCommandContext, TInjected>>();
}