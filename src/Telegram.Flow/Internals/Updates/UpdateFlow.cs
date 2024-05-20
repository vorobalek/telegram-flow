using System.Diagnostics;
using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates;

namespace Telegram.Flow.Internals.Updates;

[DebuggerDisplay("{DisplayName}")]
internal class UpdateFlow(
    ICollection<UpdateType> targetTypes,
    IEnumerable<IMessageFlow> messageFlows,
    IEnumerable<ICallbackQueryFlow> callbackQueryFlows,
    IEnumerable<IEditedMessageFlow> editedMessageFlows,
    IEnumerable<AsyncProcessingDelegate<IUpdateContext>> tasks,
    string? displayName = null) : 
    Flow<IUpdateContext>(tasks),
    IUpdateFlow
{
    public string? DisplayName { get; } = displayName;

    public override async Task ProcessAsync(IUpdateContext context, CancellationToken cancellationToken)
    {
        if (!targetTypes.Contains(context.Update.Type)) return;

        switch (context.Update.Type)
        {
            case UpdateType.Message when context.Update is { Message: not null }:
                await Task.WhenAll(messageFlows.Select(flow =>
                    flow.ProcessAsync(
                        new MessageContext(context.Update, context.Update.Message),
                        cancellationToken)));
                break;
            case UpdateType.CallbackQuery when context.Update is {CallbackQuery: not null}:
                await Task.WhenAll(callbackQueryFlows.Select(flow =>
                    flow.ProcessAsync(
                        new CallbackQueryContext(context.Update, context.Update.CallbackQuery),
                        cancellationToken)));
                break;
            case UpdateType.EditedMessage when context.Update is { EditedMessage: not null }:
                await Task.WhenAll(editedMessageFlows.Select(flow =>
                    flow.ProcessAsync(
                        new EditedMessageContext(context.Update, context.Update.EditedMessage),
                        cancellationToken)));
                break;
            default:
                return;
        }

        await base.ProcessAsync(context, cancellationToken);
    }
}

internal class UpdateFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IUpdateContext, TInjected>> injectedTasks,
    ICollection<UpdateType> targetTypes,
    IEnumerable<IMessageFlow> messageFlows,
    IEnumerable<ICallbackQueryFlow> callbackQueryFlows,
    IEnumerable<IEditedMessageFlow> editedMessageFlows,
    IEnumerable<AsyncProcessingDelegate<IUpdateContext>> tasks,
    string? displayName = null) : 
    UpdateFlow(
        targetTypes, 
        messageFlows,
        callbackQueryFlows,
        editedMessageFlows,
        tasks, 
        displayName), 
    IUpdateFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(IUpdateContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}