using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageUpdateHandler(
    ICollection<MessageType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext>> processingDelegates,
    IEnumerable<ITextEditedMessageUpdateHandler> textEditedMessageUpdateHandlers)
    : IEditedMessageUpdateHandler
{
    public async Task ProcessAsync(IEditedMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        if (!targetTypes.Contains(context.EditedMessage.Type)) return;

        switch (context.EditedMessage.Type)
        {
            case MessageType.Text when context.EditedMessage is { Text: not null }:
                await Task.WhenAll(textEditedMessageUpdateHandlers.Select(handler =>
                    handler.ProcessAsync(
                        new TextEditedMessageUpdateHandlerContext(
                            context.Update,
                            context.EditedMessage,
                            context.EditedMessage.Text),
                        cancellationToken)));
                break;
            default:
                return;
        }

        await ProcessInternalAsync(context, cancellationToken);
    }

    protected virtual Task ProcessInternalAsync(IEditedMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class EditedMessageUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    ICollection<MessageType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext>> processingDelegates,
    IEnumerable<ITextEditedMessageUpdateHandler> textEditedMessageUpdateHandlers) :
    EditedMessageUpdateHandler(
        targetTypes,
        processingDelegates,
        textEditedMessageUpdateHandlers),
    IEditedMessageUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(IEditedMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}