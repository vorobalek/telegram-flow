using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal class TextEditedMessageUpdateHandler(
    IEnumerable<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext>> processingDelegates)
    : ITextEditedMessageUpdateHandler
{
    public async Task ProcessAsync(ITextEditedMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        await ProcessInternalAsync(context, cancellationToken);
    }

    protected virtual Task ProcessInternalAsync(ITextEditedMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class TextEditedMessageUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    IEnumerable<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext>> processingDelegates) :
    TextEditedMessageUpdateHandler(
        processingDelegates),
    ITextEditedMessageUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(ITextEditedMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}