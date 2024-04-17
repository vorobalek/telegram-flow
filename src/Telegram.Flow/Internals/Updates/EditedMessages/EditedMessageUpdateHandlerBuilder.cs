using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageUpdateHandlerBuilder : IEditedMessageUpdateHandlerBuilder
{
    public ISet<MessageType> TargetEditedMessageTypes { get; protected init; } = new SortedSet<MessageType>();

    public IList<ITextEditedMessageUpdateHandlerBuilder> TextEditedMessageUpdateHandlerBuilders { get; protected init; } =
        new List<ITextEditedMessageUpdateHandlerBuilder>();

    public IList<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext>> ProcessingTasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext>>();
}

internal class EditedMessageUpdateHandlerBuilder<TInjected> :
    EditedMessageUpdateHandlerBuilder,
    IEditedMessageUpdateHandlerBuilder<TInjected>
{
    public EditedMessageUpdateHandlerBuilder(IEditedMessageUpdateHandlerBuilder prototypeBuilder)
    {
        TargetEditedMessageTypes = prototypeBuilder.TargetEditedMessageTypes;
        TextEditedMessageUpdateHandlerBuilders = prototypeBuilder.TextEditedMessageUpdateHandlerBuilders;
        ProcessingTasks = prototypeBuilder.ProcessingTasks;
    }
    
    public IList<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext, TInjected>> InjectedProcessingTasks { get; } =
        new List<AsyncProcessingDelegate<IEditedMessageUpdateHandlerContext, TInjected>>();
}