using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates;

internal class UpdateHandlerBuilder : IUpdateHandlerBuilder
{
    public string? DisplayName { get; set; }

    public ISet<UpdateType> TargetUpdateTypes { get; protected init; } = new SortedSet<UpdateType>();

    public IList<IMessageUpdateHandlerBuilder> MessageUpdateHandlerBuilders { get; protected init; } =
        new List<IMessageUpdateHandlerBuilder>();

    public IList<ICallbackQueryUpdateHandlerBuilder> CallbackQueryUpdateHandlerBuilders { get; protected init; } =
        new List<ICallbackQueryUpdateHandlerBuilder>();

    public IList<IEditedMessageUpdateHandlerBuilder> EditedMessageUpdateHandlerBuilders { get; protected init; } =
        new List<IEditedMessageUpdateHandlerBuilder>();

    public IList<AsyncProcessingDelegate<IUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IUpdateHandlerContext>>();
}

internal class UpdateHandlerBuilder<TInjected> : UpdateHandlerBuilder, IUpdateHandlerBuilder<TInjected>
{
    public UpdateHandlerBuilder(IUpdateHandlerBuilder prototypeBuilder)
    {
        DisplayName = prototypeBuilder.DisplayName;
        TargetUpdateTypes = prototypeBuilder.TargetUpdateTypes;
        MessageUpdateHandlerBuilders = prototypeBuilder.MessageUpdateHandlerBuilders;
        CallbackQueryUpdateHandlerBuilders = prototypeBuilder.CallbackQueryUpdateHandlerBuilders;
        EditedMessageUpdateHandlerBuilders = prototypeBuilder.EditedMessageUpdateHandlerBuilders;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }

    public IList<AsyncProcessingDelegate<IUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<IUpdateHandlerContext, TInjected>>();
}