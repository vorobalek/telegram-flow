using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageFlow(
    ICollection<MessageType> targetTypes,
    IEnumerable<ITextFlow> textFlows,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageContext>> tasks) :
    Flow<IEditedMessageContext>(tasks),
    IEditedMessageFlow
{
    public override async Task ProcessAsync(IEditedMessageContext context, CancellationToken cancellationToken)
    {
        if (!targetTypes.Contains(context.EditedMessage.Type)) return;

        switch (context.EditedMessage.Type)
        {
            case MessageType.Text when context.EditedMessage is { Text: not null }:
                await Task.WhenAll(textFlows.Select(flow =>
                    flow.ProcessAsync(
                        new TextContext(
                            context.Update,
                            context.EditedMessage,
                            context.EditedMessage.Text),
                        cancellationToken)));
                break;
            default:
                return;
        }

        await base.ProcessAsync(context, cancellationToken);
    }
}

internal class EditedMessageFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageContext, TInjected>> injectedTasks,
    ICollection<MessageType> targetTypes,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageContext>> tasks,
    IEnumerable<ITextFlow> textFlows) :
    EditedMessageFlow(
        targetTypes,
        textFlows,
        tasks),
    IEditedMessageFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(IEditedMessageContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(processingDelegate =>
            processingDelegate.Invoke(context, injected, cancellationToken)));
    }
}