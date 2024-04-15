using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;

namespace Telegram.Flow.Updates.Messages;

public interface IMessageUpdateHandlerBuilder : IAsyncProcessingBuilder<IMessageUpdateHandlerContext>
{
    ISet<MessageType> TargetMessageTypes { get; }

    IList<ITextMessageUpdateHandlerBuilder> TextMessageUpdateHandlerBuilders { get; }
}

public interface IMessageUpdateHandlerBuilder<TInjected> :
    IMessageUpdateHandlerBuilder,
    IAsyncProcessingBuilder<IMessageUpdateHandlerContext, TInjected>;