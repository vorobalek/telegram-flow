using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal class TextFlow(
    IEnumerable<IBotCommandFlow> botCommandFlows,
    IEnumerable<AsyncProcessingDelegate<ITextContext>> tasks) :
    Flow<ITextContext>(tasks),
    ITextFlow
{
    public override async Task ProcessAsync(ITextContext context, CancellationToken cancellationToken)
    {
        if (context.Message.Entities is { } entities)
        {
            var commands = entities.Where(entity => entity.Type is MessageEntityType.BotCommand).ToArray();
            for (var index = 0; index < entities.Length; ++index)
            {
                var startCommandIndex = commands[index].Offset + 1;
                var endCommandIndex = commands[index].Offset + commands[index].Length;
                var endDataIndex = index + 1 < entities.Length
                    ? commands[index + 1].Offset
                    : context.Text.Length;
                var command = context.Text[startCommandIndex..endCommandIndex].Trim();
                var data = context.Text[endCommandIndex..endDataIndex].Trim();
                var isLeading = commands[index].Offset == 0;

                await Task.WhenAll(botCommandFlows.Select(flow =>
                    flow.ProcessAsync(
                        new BotCommandContext(
                            context.Update,
                            context.Message,
                            context.Text,
                            command,
                            data,
                            isLeading
                        ),
                        cancellationToken)));
            }
        }

        await base.ProcessAsync(context, cancellationToken);
    }
}

internal class TextFlow<TInjected>(
    TInjected injected,
    IEnumerable<AsyncProcessingDelegate<ITextContext, TInjected>> injectedTasks,
    IEnumerable<AsyncProcessingDelegate<ITextContext>> tasks,
    IEnumerable<IBotCommandFlow> botCommandFlows) :
    TextFlow(
        botCommandFlows,
        tasks),
    ITextFlow<TInjected>
{
    protected override async Task ProcessInternalAsync(ITextContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(injectedTasks.Select(processingDelegate =>
            processingDelegate.Invoke(context, injected, cancellationToken)));
    }
}