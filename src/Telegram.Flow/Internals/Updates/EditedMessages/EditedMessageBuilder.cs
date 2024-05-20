using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages;

internal class EditedMessageBuilder : Builder<IEditedMessageContext>, IEditedMessageBuilder
{
    public ISet<MessageType> TargetTypes { get; protected init; } = new SortedSet<MessageType>();

    public ICollection<ITextBuilder> TextBuilders { get; protected init; } =
        new List<ITextBuilder>();
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
    
    public ICollection<AsyncProcessingDelegate<IEditedMessageContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<IEditedMessageContext, TInjected>>();
}