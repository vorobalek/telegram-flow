using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal class TextBuilder : Builder<ITextContext>, ITextBuilder;

internal class TextBuilder<TInjected> :
    TextBuilder,
    ITextBuilder<TInjected>
{
    public TextBuilder(ITextBuilder prototypeBuilder)
    {
        Tasks = prototypeBuilder.Tasks;
    }
    
    public ICollection<AsyncProcessingDelegate<ITextContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ITextContext, TInjected>>();
}