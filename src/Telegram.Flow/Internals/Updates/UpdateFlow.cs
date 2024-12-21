using System.Diagnostics;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates;

[DebuggerDisplay("{DisplayName}")]
internal class UpdateFlow(
    ICollection<UpdateType> targetTypes,
    IEnumerable<IFlow<IMessageContext>> messageFlows,
    IEnumerable<IFlow<ICallbackQueryContext>> callbackQueryFlows,
    IEnumerable<IFlow<IEditedMessageContext>> editedMessageFlows,
    IEnumerable<AsyncProcessingDelegate<IUpdateContext>> tasks,
    string? displayName = null) :
    Flow<IUpdateContext>(tasks),
    IUpdateFlow
{
    public string? DisplayName { get; } = displayName;

    public override Task ProcessAsync(IUpdateContext context, CancellationToken cancellationToken)
    {
        return ProcessAsync(context.Update, cancellationToken);
    }

    public async Task ProcessAsync(Update update, CancellationToken cancellationToken)
    {
        if (targetTypes.Contains(update.Type))
        {
            switch (update.Type)
            {
                case UpdateType.Message when update is { Message: not null }:
                    await Task.WhenAll(messageFlows.Select(flow =>
                        flow.ProcessAsync(
                            new MessageContext(update, update.Message),
                            cancellationToken)));
                    break;
                case UpdateType.CallbackQuery when update is { CallbackQuery: not null }:
                    await Task.WhenAll(callbackQueryFlows.Select(flow =>
                        flow.ProcessAsync(
                            new CallbackQueryContext(update, update.CallbackQuery),
                            cancellationToken)));
                    break;
                case UpdateType.EditedMessage when update is { EditedMessage: not null }:
                    await Task.WhenAll(editedMessageFlows.Select(flow =>
                        flow.ProcessAsync(
                            new EditedMessageContext(update, update.EditedMessage),
                            cancellationToken)));
                    break;
                default:
                    return;
            }
        }

        await base.ProcessAsync(new UpdateContext(update), cancellationToken);
    }
}

internal sealed class UpdateFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IUpdateContext, TInjected>> injectedTasks,
    ICollection<UpdateType> targetTypes,
    IEnumerable<IFlow<IMessageContext>> messageFlows,
    IEnumerable<IFlow<ICallbackQueryContext>> callbackQueryFlows,
    IEnumerable<IFlow<IEditedMessageContext>> editedMessageFlows,
    IEnumerable<AsyncProcessingDelegate<IUpdateContext>> tasks,
    string? displayName = null) :
    UpdateFlow(
        targetTypes,
        messageFlows,
        callbackQueryFlows,
        editedMessageFlows,
        tasks,
        displayName)
{
    protected override async Task ProcessInternalAsync(IUpdateContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}