using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal class BotCommandFlow(
    bool allowInline,
    ISet<string> targetCommands,
    ISet<string> targetPrefixes,
    IEnumerable<AsyncProcessingDelegate<IBotCommandContext>> tasks) : 
    Flow<IBotCommandContext>(tasks),
    IBotCommandFlow
{
    public override async Task ProcessAsync(IBotCommandContext context, CancellationToken cancellationToken)
    {
        if ((targetCommands.Contains(context.Command) ||
             targetPrefixes.Any(targetPrefix => context.Command.StartsWith(targetPrefix))) &&
            (context.IsLeading || allowInline))
        {
            await base.ProcessAsync(context, cancellationToken);
        }
    }
}

internal class BotCommandFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IBotCommandContext, TInjected>> injectedTasks,
    bool allowInline,
    ISet<string> targetCommands,
    ISet<string> targetPrefixes,
    IEnumerable<AsyncProcessingDelegate<IBotCommandContext>> tasks) : 
    BotCommandFlow(allowInline, targetCommands, targetPrefixes, tasks),
    IBotCommandFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(IBotCommandContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}