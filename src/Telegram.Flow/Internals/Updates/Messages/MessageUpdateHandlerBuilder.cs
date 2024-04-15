using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages;

internal class MessageUpdateHandlerBuilder : IMessageUpdateHandlerBuilder
{
    public ISet<MessageType> TargetMessageTypes { get; protected init; } = new SortedSet<MessageType>();

    public IList<ITextMessageUpdateHandlerBuilder> TextMessageUpdateHandlerBuilders { get; protected init; } =
        new List<ITextMessageUpdateHandlerBuilder>();

    public IList<AsyncProcessingDelegate<IMessageUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IMessageUpdateHandlerContext>>();
}

internal class MessageUpdateHandlerBuilder<TInjected> :
    MessageUpdateHandlerBuilder,
    IMessageUpdateHandlerBuilder<TInjected>
{
    public MessageUpdateHandlerBuilder(IMessageUpdateHandlerBuilder prototypeBuilder)
    {
        TargetMessageTypes = prototypeBuilder.TargetMessageTypes;
        TextMessageUpdateHandlerBuilders = prototypeBuilder.TextMessageUpdateHandlerBuilders;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<IMessageUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<IMessageUpdateHandlerContext, TInjected>>();
}