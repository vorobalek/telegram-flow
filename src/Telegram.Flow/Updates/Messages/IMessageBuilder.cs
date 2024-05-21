using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Updates.Messages;

public interface IMessageBuilder :
    IBuilder<IMessageContext>
{
    internal ISet<MessageType> TargetTypes { get; }

    internal ICollection<ITextBuilder> TextBuilders { get; }

    public IMessageBuilder<TInjected> WithInjection<TInjected>(TInjected injected);
}

public interface IMessageBuilder<TInjected> :
    IMessageBuilder,
    IBuilder<IMessageContext, TInjected>;