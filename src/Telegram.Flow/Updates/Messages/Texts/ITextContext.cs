namespace Telegram.Flow.Updates.Messages.Texts;

public interface ITextContext : IMessageContext
{
    string Text { get; }
}