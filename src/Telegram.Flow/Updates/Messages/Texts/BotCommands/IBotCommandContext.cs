namespace Telegram.Flow.Updates.Messages.Texts.BotCommands;

public interface IBotCommandContext : ITextContext
{
    string Command { get; }
    string Data { get; }
    bool IsLeading { get; }
}