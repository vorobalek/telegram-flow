using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates;

internal class UpdateBuilder :
    Builder<IUpdateContext>,
    IUpdateBuilder
{
    public string? DisplayName { get; set; }

    public ISet<UpdateType> TargetUpdateTypes { get; protected init; } = new SortedSet<UpdateType>();

    public ICollection<IMessageBuilder> MessageBuilders { get; protected init; } =
        new List<IMessageBuilder>();

    public ICollection<ICallbackQueryBuilder> CallbackQueryBuilders { get; protected init; } =
        new List<ICallbackQueryBuilder>();

    public ICollection<IEditedMessageBuilder> EditedMessageBuilders { get; protected init; } =
        new List<IEditedMessageBuilder>();

    public IUpdateBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new UpdateBuilder<TInjected>(this, injected);
    }

    public override IUpdateFlow Build()
    {
        var (
            messageFlows,
            callbackQueryFlows,
            editedMessageFlows
            ) = BuildDependencies();

        return new UpdateFlow(
            TargetUpdateTypes,
            messageFlows,
            callbackQueryFlows,
            editedMessageFlows,
            Tasks,
            DisplayName);
    }

    protected (
        IEnumerable<IFlow<IMessageContext>> messageFlows,
        IEnumerable<IFlow<ICallbackQueryContext>> callbackQueryFlows,
        IEnumerable<IFlow<IEditedMessageContext>> editedMessageFlows)
        BuildDependencies()
    {
        return (
            MessageBuilders.Select(builder => builder.Build()),
            CallbackQueryBuilders.Select(builder => builder.Build()),
            EditedMessageBuilders.Select(builder => builder.Build())
        );
    }
}

internal sealed class UpdateBuilder<TInjected> :
    UpdateBuilder,
    IUpdateBuilder<TInjected>
{
    public UpdateBuilder(IUpdateBuilder prototypeBuilder, TInjected injected)
    {
        DisplayName = prototypeBuilder.DisplayName;
        TargetUpdateTypes = prototypeBuilder.TargetUpdateTypes;
        MessageBuilders = prototypeBuilder.MessageBuilders;
        CallbackQueryBuilders = prototypeBuilder.CallbackQueryBuilders;
        EditedMessageBuilders = prototypeBuilder.EditedMessageBuilders;
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<IUpdateContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IUpdateContext, TInjected>>();

    public override IUpdateFlow Build()
    {
        var (
            messageFlows,
            callbackQueryFlows,
            editedMessageFlows
            ) = BuildDependencies();

        return new UpdateFlow<TInjected>(
            Injected,
            InjectedTasks,
            TargetUpdateTypes,
            messageFlows,
            callbackQueryFlows,
            editedMessageFlows,
            Tasks,
            DisplayName);
    }
}