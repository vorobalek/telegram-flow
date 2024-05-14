using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Updates.EditedMessages;

public interface IEditedMessageBuilder : IBuilder<IEditedMessageContext>
{
    internal ISet<MessageType> TargetTypes { get; }

    internal IList<ITextBuilder> TextBuilders { get; }
}

public interface IEditedMessageBuilder<TInjected> :
    IEditedMessageBuilder,
    IBuilder<IEditedMessageContext, TInjected>;