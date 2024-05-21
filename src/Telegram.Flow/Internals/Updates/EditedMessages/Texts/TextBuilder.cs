using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.EditedMessages.Texts;

namespace Telegram.Flow.Internals.Updates.EditedMessages.Texts;

internal class TextBuilder :
    Builder<ITextContext>,
    ITextBuilder
{
    public ITextBuilder<TInjected> WithInjection<TInjected>(TInjected injected)
    {
        return new TextBuilder<TInjected>(this, injected);
    }

    public override IFlow<ITextContext> Build()
    {
        return new TextFlow(Tasks);
    }
}

internal sealed class TextBuilder<TInjected> :
    TextBuilder,
    ITextBuilder<TInjected>
{
    public TextBuilder(ITextBuilder prototypeBuilder, TInjected injected)
    {
        Tasks = prototypeBuilder.Tasks;
        Injected = injected;
    }

    public TInjected Injected { get; }

    public ICollection<AsyncProcessingDelegate<ITextContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ITextContext, TInjected>>();

    public override IFlow<ITextContext> Build()
    {
        return new TextFlow<TInjected>(
            Injected,
            InjectedTasks,
            Tasks);
    }
}