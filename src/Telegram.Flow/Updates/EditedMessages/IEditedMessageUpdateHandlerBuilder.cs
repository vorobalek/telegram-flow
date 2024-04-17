using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Updates.EditedMessages;

public interface IEditedMessageUpdateHandlerBuilder : IAsyncProcessingBuilder<IEditedMessageUpdateHandlerContext>
{
    internal ISet<MessageType> TargetEditedMessageTypes { get; }

    internal IList<ITextEditedMessageUpdateHandlerBuilder> TextEditedMessageUpdateHandlerBuilders { get; }
}

public interface IEditedMessageUpdateHandlerBuilder<TInjected> :
    IEditedMessageUpdateHandlerBuilder,
    IAsyncProcessingBuilder<IEditedMessageUpdateHandlerContext, TInjected>;