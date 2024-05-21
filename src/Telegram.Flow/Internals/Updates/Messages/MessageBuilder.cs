using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages;

internal class MessageBuilder :
    Builder<IMessageContext>,
    IMessageBuilder
{
    public ISet<MessageType> TargetTypes { get; protected init; } = new SortedSet<MessageType>();

    public ICollection<ITextBuilder> TextBuilders { get; protected init; } =
        new List<ITextBuilder>();

    public IMessageBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new MessageBuilder<TInjected>(this, injected);
    }

    public override IFlow<IMessageContext> Build()
    {
        var textFlows = BuildDependencies();

        return new MessageFlow(
            TargetTypes,
            textFlows,
            Tasks);
    }

    protected IEnumerable<IFlow<ITextContext>> BuildDependencies()
    {
        return TextBuilders.Select(builder => builder.Build());
    }
}

internal sealed class MessageBuilder<TInjected> :
    MessageBuilder,
    IMessageBuilder<TInjected>
{
    public MessageBuilder(IMessageBuilder prototypeBuilder, TInjected injected)
    {
        TargetTypes = prototypeBuilder.TargetTypes;
        TextBuilders = prototypeBuilder.TextBuilders;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<IMessageContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IMessageContext, TInjected>>();

    public override IFlow<IMessageContext> Build()
    {
        var textFlows = BuildDependencies();

        return new MessageFlow<TInjected>(
            Injected,
            InjectedTasks,
            TargetTypes,
            textFlows,
            Tasks);
    }
}