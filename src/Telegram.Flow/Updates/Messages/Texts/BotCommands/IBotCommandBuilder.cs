using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.Messages.Texts.BotCommands;

public interface IBotCommandBuilder :
    IBuilder<IBotCommandContext>
{
    internal bool AllowInline { get; set; }
    internal ISet<string> TargetCommands { get; }
    internal ISet<string> TargetCommandPrefixes { get; }

    public IBotCommandBuilder<TInjected> WithInjection<TInjected>(TInjected injected);
}

public interface IBotCommandBuilder<TInjected> :
    IBotCommandBuilder,
    IBuilder<IBotCommandContext, TInjected>;