using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.EditedMessages.Texts;

public interface ITextBuilder :
    IBuilder<ITextContext>
{
    public ITextBuilder<TInjected> WithInjection<TInjected>(TInjected injected);
}

public interface ITextBuilder<TInjected> :
    ITextBuilder,
    IBuilder<ITextContext, TInjected>;