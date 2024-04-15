using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal class TextEditedMessageUpdateHandlerBuilder : ITextEditedMessageUpdateHandlerBuilder
{
    public IList<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext>>();
}

internal class TextEditedMessageUpdateHandlerBuilder<TInjected> :
    TextEditedMessageUpdateHandlerBuilder,
    ITextEditedMessageUpdateHandlerBuilder<TInjected>
{
    public TextEditedMessageUpdateHandlerBuilder(ITextEditedMessageUpdateHandlerBuilder prototypeBuilder)
    {
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<ITextEditedMessageUpdateHandlerContext, TInjected>>();
}