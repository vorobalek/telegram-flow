using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.EditedMessages.Texts;

public interface ITextBuilder : IBuilder<ITextContext>
{
}

public interface ITextBuilder<TInjected> :
    ITextBuilder,
    IBuilder<ITextContext, TInjected>;