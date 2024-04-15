using System.Diagnostics;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates;

namespace Telegram.Flow.Internals.Updates;

[DebuggerDisplay("{DisplayName}")]
internal class UpdateHandler(
    ICollection<UpdateType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IUpdateHandlerContext>> processingDelegates,
    IEnumerable<IMessageUpdateHandler> messageUpdateHandlers,
    IEnumerable<ICallbackQueryUpdateHandler> callbackQueryUpdateHandlers,
    IEnumerable<IEditedMessageUpdateHandler> editedMessageUpdateHandlers,
    string? displayName = null)
    : IUpdateHandler
{
    public string? DisplayName { get; } = displayName;

    public async Task ProcessAsync(Update update, CancellationToken cancellationToken)
    {
        if (!targetTypes.Contains(update.Type)) return;

        switch (update.Type)
        {
            case UpdateType.Message when update is { Message: not null }:
                await Task.WhenAll(messageUpdateHandlers.Select(handler =>
                    handler.ProcessAsync(
                        new MessageUpdateHandlerContext(update, update.Message),
                        cancellationToken)));
                break;
            case UpdateType.CallbackQuery when update is {CallbackQuery: not null}:
                await Task.WhenAll(callbackQueryUpdateHandlers.Select(handler =>
                    handler.ProcessAsync(
                        new CallbackQueryUpdateHandlerContext(update, update.CallbackQuery),
                        cancellationToken)));
                break;
            case UpdateType.EditedMessage when update is { EditedMessage: not null }:
                await Task.WhenAll(editedMessageUpdateHandlers.Select(handler =>
                    handler.ProcessAsync(
                        new EditedMessageUpdateHandlerContext(update, update.EditedMessage),
                        cancellationToken)));
                break;
            default:
                return;
        }

        await ProcessInternalAsync(new UpdateHandlerContext(update), cancellationToken);
    }

    protected virtual Task ProcessInternalAsync(IUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class UpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<IUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    ICollection<UpdateType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IUpdateHandlerContext>> processingDelegates,
    IEnumerable<IMessageUpdateHandler> messageUpdateHandlers,
    IEnumerable<ICallbackQueryUpdateHandler> callbackQueryUpdateHandlers,
    IEnumerable<IEditedMessageUpdateHandler> editedMessageUpdateHandlers,
    string? displayName = null) : 
    UpdateHandler(
        targetTypes, 
        processingDelegates, 
        messageUpdateHandlers,
        callbackQueryUpdateHandlers,
        editedMessageUpdateHandlers,
        displayName), 
    IUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(IUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}