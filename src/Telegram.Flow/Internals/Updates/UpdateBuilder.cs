using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates;

internal class UpdateBuilder : IUpdateBuilder
{
    public string? DisplayName { get; set; }

    public ISet<UpdateType> TargetUpdateTypes { get; protected init; } = new SortedSet<UpdateType>();

    public IList<IMessageBuilder> MessageBuilders { get; protected init; } =
        new List<IMessageBuilder>();

    public IList<ICallbackQueryBuilder> CallbackQueryBuilders { get; protected init; } =
        new List<ICallbackQueryBuilder>();

    public IList<IEditedMessageBuilder> EditedMessageBuilders { get; protected init; } =
        new List<IEditedMessageBuilder>();

    public IList<AsyncProcessingDelegate<IUpdateContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IUpdateContext>>();
}

internal class UpdateBuilder<TInjected> : UpdateBuilder, IUpdateBuilder<TInjected>
{
    public UpdateBuilder(IUpdateBuilder prototypeBuilder)
    {
        DisplayName = prototypeBuilder.DisplayName;
        TargetUpdateTypes = prototypeBuilder.TargetUpdateTypes;
        MessageBuilders = prototypeBuilder.MessageBuilders;
        CallbackQueryBuilders = prototypeBuilder.CallbackQueryBuilders;
        EditedMessageBuilders = prototypeBuilder.EditedMessageBuilders;
        Tasks = prototypeBuilder.Tasks;
    }

    public IList<AsyncProcessingDelegate<IUpdateContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IUpdateContext, TInjected>>();
}