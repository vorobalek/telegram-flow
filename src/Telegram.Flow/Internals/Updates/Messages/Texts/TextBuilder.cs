using Telegram.Flow.Infrastructure;
using Telegram.Flow.Infrastructure.Internals;
using Telegram.Flow.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts;

internal class TextBuilder : Builder<ITextContext>, ITextBuilder
{
    public ICollection<IBotCommandBuilder> BotCommandBuilders { get; protected init; } =
        new List<IBotCommandBuilder>();
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
    
    public ICollection<AsyncProcessingDelegate<ITextContext, TInjected>> InjectedTasks { get; } =
        new List<AsyncProcessingDelegate<ITextContext, TInjected>>();
}