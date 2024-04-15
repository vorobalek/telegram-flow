namespace Telegram.Flow.Updates.EditedMessages.Texts;

public interface ITextEditedMessageUpdateHandlerContext : IEditedMessageUpdateHandlerContext
{
    string Text { get; }
}