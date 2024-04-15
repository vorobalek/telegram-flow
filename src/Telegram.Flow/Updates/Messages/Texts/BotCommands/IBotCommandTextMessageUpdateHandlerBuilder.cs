using Telegram.Flow.Infrastructure;

namespace Telegram.Flow.Updates.Messages.Texts.BotCommands;

public interface IBotCommandTextMessageUpdateHandlerBuilder : 
    IAsyncProcessingBuilder<IBotCommandTextMessageUpdateHandlerContext>
{
    internal bool AllowInline { get; set; }
    internal ISet<string> TargetCommands { get; }
    internal ISet<string> TargetCommandPrefixes { get; }
}

public interface IBotCommandTextMessageUpdateHandlerBuilder<TInjected> : 
    IBotCommandTextMessageUpdateHandlerBuilder, 
    IAsyncProcessingBuilder<IBotCommandTextMessageUpdateHandlerContext, TInjected>;