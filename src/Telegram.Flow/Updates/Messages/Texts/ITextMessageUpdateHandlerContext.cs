namespace Telegram.Flow.Updates.Messages.Texts;

public interface ITextMessageUpdateHandlerContext : IMessageUpdateHandlerContext
{
    string Text { get; }
}