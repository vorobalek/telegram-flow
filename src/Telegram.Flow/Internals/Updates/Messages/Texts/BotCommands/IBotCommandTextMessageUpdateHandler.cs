using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal interface IBotCommandTextMessageUpdateHandler : IHandler<IBotCommandTextMessageUpdateHandlerContext>;

internal interface IBotCommandTextMessageUpdateHandler<TInjected> : IBotCommandTextMessageUpdateHandler;