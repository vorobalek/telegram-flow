using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal class TextBuilder : ITextBuilder
{
    public IList<AsyncProcessingDelegate<ITextContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<ITextContext>>();
}

internal class TextBuilder<TInjected> :
    TextBuilder,
    ITextBuilder<TInjected>
{
    public TextBuilder(ITextBuilder prototypeBuilder)
    {
        Tasks = prototypeBuilder.Tasks;
    }
    
    public IList<AsyncProcessingDelegate<ITextContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ITextContext, TInjected>>();
}