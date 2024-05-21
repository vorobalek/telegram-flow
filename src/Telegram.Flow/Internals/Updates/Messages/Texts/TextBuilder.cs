using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal class TextBuilder :
    Builder<ITextContext>,
    ITextBuilder
{
    public ICollection<IBotCommandBuilder> BotCommandBuilders { get; protected init; } =
        new List<IBotCommandBuilder>();

    public ITextBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new TextBuilder<TInjected>(this, injected);
    }

    public override IFlow<ITextContext> Build()
    {
        var botCommandFlows = BuildDependencies();

        return new TextFlow(
            botCommandFlows,
            Tasks);
    }

    protected IEnumerable<IFlow<IBotCommandContext>> BuildDependencies()
    {
        return BotCommandBuilders.Select(builder => builder.Build());
    }
}

internal sealed class TextBuilder<TInjected> :
    TextBuilder,
    ITextBuilder<TInjected>
{
    public TextBuilder(ITextBuilder prototypeBuilder, TInjected injected)
    {
        BotCommandBuilders = prototypeBuilder.BotCommandBuilders;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<ITextContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ITextContext, TInjected>>();

    public override IFlow<ITextContext> Build()
    {
        var botCommandFlows = BuildDependencies();

        return new TextFlow<TInjected>(
            Injected,
            InjectedTasks,
            botCommandFlows,
            Tasks);
    }
}