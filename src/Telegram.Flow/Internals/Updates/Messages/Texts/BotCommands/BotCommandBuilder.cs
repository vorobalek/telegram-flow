using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal class BotCommandBuilder :
    Builder<IBotCommandContext>,
    IBotCommandBuilder
{
    public bool AllowInline { get; set; }
    public ISet<string> TargetCommands { get; protected init; } = new SortedSet<string>();
    public ISet<string> TargetCommandPrefixes { get; protected init; } = new SortedSet<string>();

    public IBotCommandBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new BotCommandBuilder<TInjected>(this, injected);
    }

    public override IFlow<IBotCommandContext> Build()
    {
        return new BotCommandFlow(
            AllowInline,
            TargetCommands,
            TargetCommandPrefixes,
            Tasks);
    }
}

internal sealed class BotCommandBuilder<TInjected> :
    BotCommandBuilder,
    IBotCommandBuilder<TInjected>
{
    public BotCommandBuilder(IBotCommandBuilder prototypeBuilder, TInjected injected)
    {
        AllowInline = prototypeBuilder.AllowInline;
        TargetCommands = prototypeBuilder.TargetCommands;
        TargetCommandPrefixes = prototypeBuilder.TargetCommandPrefixes;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<IBotCommandContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IBotCommandContext, TInjected>>();

    public override IFlow<IBotCommandContext> Build()
    {
        return new BotCommandFlow<TInjected>(
            Injected,
            InjectedTasks,
            AllowInline,
            TargetCommands,
            TargetCommandPrefixes,
            Tasks);
    }
}