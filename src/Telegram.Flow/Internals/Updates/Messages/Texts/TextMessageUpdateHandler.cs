using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal class TextMessageUpdateHandler(
    IEnumerable<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext>> processingDelegates,
    IEnumerable<IBotCommandTextMessageUpdateHandler> botCommandTextMessageUpdateHandlers)
    : ITextMessageUpdateHandler
{
    public async Task ProcessAsync(ITextMessageUpdateHandlerContext context, CancellationToken cancellationToken)
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

                await Task.WhenAll(botCommandTextMessageUpdateHandlers.Select(botCommandTextMessageUpdateHandler =>
                    botCommandTextMessageUpdateHandler.ProcessAsync(
                        new BotCommandTextMessageUpdateHandlerContext(
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

        await ProcessInternalAsync(context, cancellationToken);
    }

    protected virtual Task ProcessInternalAsync(ITextMessageUpdateHandlerContext context,
        CancellationToken cancellationToken)
    {
        return Task.WhenAll(processingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, cancellationToken)));
    }
}

internal class TextMessageUpdateHandler<TInjected>(
    TInjected tInjected,
    IEnumerable<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext, TInjected>> tInjectedProcessingDelegates,
    IEnumerable<AsyncProcessingDelegate<ITextMessageUpdateHandlerContext>> processingDelegates,
    IEnumerable<IBotCommandTextMessageUpdateHandler> botCommandTextMessageUpdateHandlers) :
    TextMessageUpdateHandler(
        processingDelegates,
        botCommandTextMessageUpdateHandlers),
    ITextMessageUpdateHandler<TInjected>
{
    protected override async Task ProcessInternalAsync(ITextMessageUpdateHandlerContext context, CancellationToken cancellationToken)
    {
        await base.ProcessInternalAsync(context, cancellationToken);
        await Task.WhenAll(tInjectedProcessingDelegates.Select(processingDelegate =>
            processingDelegate.Invoke(context, tInjected, cancellationToken)));
    }
}