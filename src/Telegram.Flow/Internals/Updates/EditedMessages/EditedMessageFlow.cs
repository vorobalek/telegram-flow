using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.EditedMessages.Texts;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageFlow(
    ICollection<MessageType> targetTypes,
    IEnumerable<IFlow<ITextContext>> textFlows,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageContext>> tasks) :
    Flow<IEditedMessageContext>(tasks)
{
    public override async Task ProcessAsync(IEditedMessageContext context, CancellationToken cancellationToken)
    {
        if (targetTypes.Contains(context.EditedMessage.Type))
        {

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
        }

        await base.ProcessAsync(context, cancellationToken);
    }
}

internal sealed class EditedMessageFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageContext, TInjected>> injectedTasks,
    ICollection<MessageType> targetTypes,
    IEnumerable<IFlow<ITextContext>> textFlows,
    IEnumerable<AsyncProcessingDelegate<IEditedMessageContext>> tasks) :
    EditedMessageFlow(
        targetTypes,
        textFlows,
        tasks)
{
    protected override async Task ProcessInternalAsync(IEditedMessageContext context,
        CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(task =>
            task.Invoke(context, injected, cancellationToken)));
    }
}