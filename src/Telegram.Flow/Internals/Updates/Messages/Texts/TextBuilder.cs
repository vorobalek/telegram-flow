using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal class TextBuilder : ITextBuilder
{
    public IList<IBotCommandBuilder> BotCommandBuilders { get; protected init; } =
        new List<IBotCommandBuilder>();

    public IList<AsyncProcessingDelegate<ITextContext>> Tasks { get; protected init; } =
        new List<AsyncProcessingDelegate<ITextContext>>();
}

internal class TextBuilder<TInjected> :
    TextBuilder,
    ITextBuilder<TInjected>
{
    public TextBuilder(ITextBuilder prototypeBuilder)
    {
        BotCommandBuilders = prototypeBuilder.BotCommandBuilders;
        Tasks = prototypeBuilder.Tasks;
    }
    
    public IList<AsyncProcessingDelegate<ITextContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ITextContext, TInjected>>();
}