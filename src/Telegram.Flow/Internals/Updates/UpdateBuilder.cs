using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates;

internal class UpdateBuilder : Builder<IUpdateContext>, IUpdateBuilder
{
    public string? DisplayName { get; set; }

    public ISet<UpdateType> TargetUpdateTypes { get; protected init; } = new SortedSet<UpdateType>();

    public ICollection<IMessageBuilder> MessageBuilders { get; protected init; } =
        new List<IMessageBuilder>();

    public ICollection<ICallbackQueryBuilder> CallbackQueryBuilders { get; protected init; } =
        new List<ICallbackQueryBuilder>();

    public ICollection<IEditedMessageBuilder> EditedMessageBuilders { get; protected init; } =
        new List<IEditedMessageBuilder>();
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

    public ICollection<AsyncProcessingDelegate<IUpdateContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IUpdateContext, TInjected>>();
}