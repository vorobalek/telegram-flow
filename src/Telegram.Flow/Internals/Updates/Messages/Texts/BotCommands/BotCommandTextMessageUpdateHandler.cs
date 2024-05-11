using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal class BotCommandTextMessageUpdateHandler(
    bool allowInline,
    ISet<string> targetCommands,
    ISet<string> targetPrefixes,
    IEnumerable<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext>> processingDelegates) : 
    IBotCommandTextMessageUpdateHandler
{
    public async Task ProcessAsync(IBotCommandTextMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        if ((targetCommands.Contains(context.Command) ||
             targetPrefixes.Any(targetPrefix => context.Command.StartsWith(targetPrefix))) &&
            (context.IsLeading || allowInline))
        {
            await ProcessInternalAsync(context, cancellationToken);
        }
    }

    protected virtual Task ProcessInternalAsync(IBotCommandTextMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class BotCommandTextMessageUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    bool allowInline,
    ISet<string> targetCommands,
    ISet<string> targetPrefixes,
    IEnumerable<AsyncProcessingDelegate<IBotCommandTextMessageUpdateHandlerContext>> processingDelegates) : 
    BotCommandTextMessageUpdateHandler(allowInline, targetCommands, targetPrefixes, processingDelegates),
    IBotCommandTextMessageUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(IBotCommandTextMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}