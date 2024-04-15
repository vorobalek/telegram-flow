using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates.Messages;

internal class MessageUpdateHandler(
    ICollection<MessageType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IMessageUpdateHandlerContext>> processingDelegates,
    IEnumerable<ITextMessageUpdateHandler> textMessageUpdateHandlers)
    : IMessageUpdateHandler
{
    public async Task ProcessAsync(IMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        if (!targetTypes.Contains(context.Message.Type)) return;

        switch (context.Message.Type)
        {
            case MessageType.Text when context.Message is { Text: not null }:
                await Task.WhenAll(textMessageUpdateHandlers.Select(handler =>
                    handler.ProcessAsync(
                        new TextMessageUpdateHandlerContext(
                            context.Update,
                            context.Message,
                            context.Message.Text),
                        cancellationToken)));
                break;
            default:
                return;
        }

        await ProcessInternalAsync(context, cancellationToken);
    }

    protected virtual Task ProcessInternalAsync(IMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class MessageUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<IMessageUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    ICollection<MessageType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IMessageUpdateHandlerContext>> processingDelegates,
    IEnumerable<ITextMessageUpdateHandler> textMessageUpdateHandlers) :
    MessageUpdateHandler(
        targetTypes,
        processingDelegates,
        textMessageUpdateHandlers),
    IMessageUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(IMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}