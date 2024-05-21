namespace Telegram.Flow.Updates.EditedMessages.Texts;

public interface ITextContext : IEditedMessageContext
{
    string Text { get; }
}