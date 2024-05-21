using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageBuilder :
    Builder<IEditedMessageContext>,
    IEditedMessageBuilder
{
    public ISet<MessageType> TargetTypes { get; protected init; } = new SortedSet<MessageType>();

    public ICollection<ITextBuilder> TextBuilders { get; protected init; } =
        new List<ITextBuilder>();

    public IEditedMessageBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new EditedMessageBuilder<TInjected>(this, injected);
    }

    public override IFlow<IEditedMessageContext> Build()
    {
        var textFlows = BuildDependencies();

        return new EditedMessageFlow(
            TargetTypes,
            textFlows,
            Tasks);
    }

    protected IEnumerable<IFlow<ITextContext>> BuildDependencies()
    {
        return TextBuilders.Select(builder => builder.Build());
    }
}

internal sealed class EditedMessageBuilder<TInjected> :
    EditedMessageBuilder,
    IEditedMessageBuilder<TInjected>
{
    public EditedMessageBuilder(IEditedMessageBuilder prototypeBuilder, TInjected injected)
    {
        TargetTypes = prototypeBuilder.TargetTypes;
        TextBuilders = prototypeBuilder.TextBuilders;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<IEditedMessageContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IEditedMessageContext, TInjected>>();

    public override IFlow<IEditedMessageContext> Build()
    {
        var textFlows = BuildDependencies();

        return new EditedMessageFlow<TInjected>(
            Injected,
            InjectedTasks,
            TargetTypes,
            textFlows,
            Tasks);
    }
}