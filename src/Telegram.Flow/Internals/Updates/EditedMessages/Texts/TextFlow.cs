using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal class TextFlow(
    IEnumerable<AsyncProcessingDelegate<ITextContext>> tasks) :
    Flow<ITextContext>(tasks),
    ITextFlow;

internal class TextFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<ITextContext, TInjected>> injectedTasks,
    IEnumerable<AsyncProcessingDelegate<ITextContext>> tasks) :
    TextFlow(tasks),
    ITextFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(ITextContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}