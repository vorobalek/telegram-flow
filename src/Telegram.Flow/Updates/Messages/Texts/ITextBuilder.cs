using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Updates.Messages.Texts;

public interface ITextBuilder :
    IBuilder<ITextContext>
{
    internal ICollection<IBotCommandBuilder> BotCommandBuilders { get; }

    public ITextBuilder<TInjected> WithInjection<TInjected>(TInjected injected);
}

public interface ITextBuilder<TInjected> :
    ITextBuilder,
    IBuilder<ITextContext, TInjected>;