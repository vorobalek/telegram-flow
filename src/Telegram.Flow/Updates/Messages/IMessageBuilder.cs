using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Updates.Messages;

public interface IMessageBuilder : IBuilder<IMessageContext>
{
    ISet<MessageType> TargetMessageTypes { get; }

    ICollection<ITextBuilder> TextBuilders { get; }
}

public interface IMessageBuilder<TInjected> :
    IMessageBuilder,
    IBuilder<IMessageContext, TInjected>;