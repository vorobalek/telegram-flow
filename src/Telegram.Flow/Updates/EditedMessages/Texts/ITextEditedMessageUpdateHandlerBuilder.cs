using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.EditedMessages.Texts;

public interface ITextEditedMessageUpdateHandlerBuilder : IAsyncProcessingBuilder<ITextEditedMessageUpdateHandlerContext>
{
}

public interface ITextEditedMessageUpdateHandlerBuilder<TInjected> :
    ITextEditedMessageUpdateHandlerBuilder,
    IAsyncProcessingBuilder<ITextEditedMessageUpdateHandlerContext, TInjected>;