using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Internals.Updates.Messages;

internal class MessageFlow(
    ICollection<MessageType> targetTypes,
    IEnumerable<ITextFlow> textFlows,
    IEnumerable<AsyncProcessingDelegate<IMessageContext>> tasks) : 
    Flow<IMessageContext>(tasks),
    IMessageFlow
{
    public override async Task ProcessAsync(IMessageContext context, CancellationToken cancellationToken)
    {
        if (!targetTypes.Contains(context.Message.Type)) return;

        switch (context.Message.Type)
        {
            case MessageType.Text when context.Message is { Text: not null }:
                await Task.WhenAll(textFlows.Select(flow =>
                    flow.ProcessAsync(
                        new TextContext(
                            context.Update,
                            context.Message,
                            context.Message.Text),
                        cancellationToken)));
                break;
            default:
                return;
        }

        await base.ProcessAsync(context, cancellationToken);
    }
}

internal class MessageFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IMessageContext, TInjected>> injectedTasks,
    ICollection<MessageType> targetTypes,
    IEnumerable<ITextFlow> textFlows,
    IEnumerable<AsyncProcessingDelegate<IMessageContext>> tasks) :
    MessageFlow(
        targetTypes,
        textFlows,
        tasks),
    IMessageFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(IMessageContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}