using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal interface IBotCommandFlow : IFlow<IBotCommandContext>;

internal interface IBotCommandFlow<TInjected> : IBotCommandFlow;