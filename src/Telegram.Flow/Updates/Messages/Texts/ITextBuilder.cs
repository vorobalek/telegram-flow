using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Updates.Messages.Texts;

public interface ITextBuilder : IBuilder<ITextContext>
{
    internal IList<IBotCommandBuilder> BotCommandBuilders { get; }
}

public interface ITextBuilder<TInjected> :
    ITextBuilder,
    IBuilder<ITextContext, TInjected>;