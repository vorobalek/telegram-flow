using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages;

internal class MessageBuilder : IMessageBuilder
{
    public ISet<MessageType> TargetMessageTypes { get; protected init; } = new SortedSet<MessageType>();

    public IList<ITextBuilder> TextBuilders { get; protected init; } =
        new List<ITextBuilder>();

    public IList<AsyncProcessingDelegate<IMessageContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IMessageContext>>();
}

internal class MessageBuilder<TInjected> :
    MessageBuilder,
    IMessageBuilder<TInjected>
{
    public MessageBuilder(IMessageBuilder prototypeBuilder)
    {
        TargetMessageTypes = prototypeBuilder.TargetMessageTypes;
        TextBuilders = prototypeBuilder.TextBuilders;
        Tasks = prototypeBuilder.Tasks;
    }
    
    public IList<AsyncProcessingDelegate<IMessageContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IMessageContext, TInjected>>();
}