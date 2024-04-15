namespace Telegram.Flow.Updates.Messages.Texts.BotCommands;

public interface IBotCommandTextMessageUpdateHandlerContext : ITextMessageUpdateHandlerContext
{
    string Command { get; }
    string Data { get; }
    bool IsLeading { get; }
}