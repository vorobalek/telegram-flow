using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageBuilder : IEditedMessageBuilder
{
    public ISet<MessageType> TargetTypes { get; protected init; } = new SortedSet<MessageType>();

    public IList<ITextBuilder> TextBuilders { get; protected init; } =
        new List<ITextBuilder>();

    public IList<AsyncProcessingDelegate<IEditedMessageContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<IEditedMessageContext>>();
}

internal class EditedMessageBuilder<TInjected> :
    EditedMessageBuilder,
    IEditedMessageBuilder<TInjected>
{
    public EditedMessageBuilder(IEditedMessageBuilder prototypeBuilder)
    {
        TargetTypes = prototypeBuilder.TargetTypes;
        TextBuilders = prototypeBuilder.TextBuilders;
        Tasks = prototypeBuilder.Tasks;
    }
    
    public IList<AsyncProcessingDelegate<IEditedMessageContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IEditedMessageContext, TInjected>>();
}