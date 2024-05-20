using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Internals.Updates.Messages;

internal class MessageBuilder : Builder<IMessageContext>, IMessageBuilder
{
    public ISet<MessageType> TargetMessageTypes { get; protected init; } = new SortedSet<MessageType>();

    public ICollection<ITextBuilder> TextBuilders { get; protected init; } =
        new List<ITextBuilder>();
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
    
    public ICollection<AsyncProcessingDelegate<IMessageContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IMessageContext, TInjected>>();
}